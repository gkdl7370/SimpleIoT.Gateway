using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SimpleIoT.Gateway.Models;
using SimpleIoT.Gateway.Utils;

namespace SimpleIoT.Gateway.Core
{
    public class GatewayEngine
    {
        private Socket? _serverSocket;
        private readonly List<Socket> _clients = new List<Socket>();
        private const int BufferSize = 1024;
        private static readonly HttpClient _httpClient = new HttpClient();
        private string _targetUrl = "";
        private Dictionary<string, string> _deviceMap = new();

        public void Start(int port, string targetUrl, Dictionary<string, string> deviceMap)
        {
            _targetUrl = targetUrl;
            _deviceMap = deviceMap;
            try
            {
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                _serverSocket.Listen(100);
                Console.WriteLine($"[Gateway] Listening on port {port}...");
                _serverSocket.BeginAccept(AcceptCallback, null);
            }
            catch (Exception ex) { Console.WriteLine($"[Error] {ex.Message}"); }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            if (_serverSocket == null) return;
            try
            {
                Socket client = _serverSocket.EndAccept(ar);
                _clients.Add(client);
                var state = new SocketState(BufferSize) { WorkingSocket = client };
                client.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReceiveCallback, state);
                _serverSocket.BeginAccept(AcceptCallback, null);
            }
            catch (Exception ex) { Console.WriteLine($"[Error] {ex.Message}"); }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            // 안전한 형변환과 null 체크 (CS8600, CS8602 해결)
            if (ar.AsyncState is not SocketState state || state.WorkingSocket == null) return;

            try
            {
                int received = state.WorkingSocket.EndReceive(ar);
                if (received > 0)
                {
                    _ = ParseRawData(state.Buffer, received, state.WorkingSocket.RemoteEndPoint?.ToString() ?? "Unknown");
                    state.ClearBuffer();
                    state.WorkingSocket.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReceiveCallback, state);
                }
            }
            catch { _clients.Remove(state.WorkingSocket); }
        }

        private async Task ParseRawData(byte[] buffer, int length, string remoteAddress)
        {
            if (length < 10 || buffer[0] != 0x02) return;
            try
            {
                string msgId = DataParser.ByteArrayToASCII(buffer, 1, 2);
                string deviceId = DataParser.ByteArrayToASCII(buffer, 8, 10);
                string siteCode = _deviceMap.GetValueOrDefault(deviceId, "UNKNOWN");

                if (msgId == "01")
                {
                    var packet = new SensorPacket
                    {
                        DeviceId = deviceId,
                        SiteCode = siteCode,
                        ValueX = BitConverter.ToSingle(buffer, 24),
                        ValueY = BitConverter.ToSingle(buffer, 28),
                        Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss")
                    };
                    await ForwardToApi(packet);
                }
            }
            catch (Exception ex) { Console.WriteLine($"[Parser Error] {ex.Message}"); }
        }

        private async Task ForwardToApi(SensorPacket packet)
        {
            try
            {
                var dto = new TelemetryData
                {
                    DeviceId = packet.DeviceId,
                    SiteCode = packet.SiteCode,
                    ValueX = packet.ValueX,
                    ValueY = packet.ValueY,
                    MeasuredAt = packet.Timestamp
                };
                var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_targetUrl, content);
                Console.WriteLine($"[API] Forwarded: {packet.DeviceId} (Status: {response.StatusCode})");
            }
            catch (Exception ex) { Console.WriteLine($"[API Error] {ex.Message}"); }
        }
    }
}