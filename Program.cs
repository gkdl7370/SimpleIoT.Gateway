using System;
using System.Threading;
using SimpleIoT.Gateway.Core;
using SimpleIoT.Gateway.Utils;

namespace SimpleIoT.Gateway
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("   SimpleIoT Gateway Service v1.0   ");
            Console.WriteLine("========================================");

            using (var exitEvent = new ManualResetEvent(false))
            {
                Console.CancelKeyPress += (s, e) => {
                    e.Cancel = true;
                    exitEvent.Set();
                };

                try
                {
                    // 1. 설정 로드
                    var config = new ConfigManager();
                    config.LoadConfigs();

                    // 2. 엔진 가동 (포트, URL, 장비맵 전달)
                    var engine = new GatewayEngine();
                    engine.Start(config.ListenPort, config.TargetApiUrl, config.DeviceMap);

                    Console.WriteLine("Gateway is running. Press Ctrl+C to stop.");
                    exitEvent.WaitOne();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Critical Error]: {ex.Message}");
                }
            }
            Console.WriteLine("Service terminated.");
        }
    }
}