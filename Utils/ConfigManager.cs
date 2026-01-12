using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleIoT.Gateway.Utils
{
    public class ConfigManager
    {
        public int ListenPort { get; private set; } = 8003;
        public string TargetApiUrl { get; private set; } = "";
        public Dictionary<string, string> DeviceMap { get; private set; } = new Dictionary<string, string>();

        public void LoadConfigs()
        {
            // 1. 게이트웨이 설정 로드
            try {
                if (File.Exists("gateway-settings.csv")) {
                    var lines = File.ReadAllLines("gateway-settings.csv");
                    foreach (var line in lines) {
                        if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line)) continue;
                        var parts = line.Split(',');
                        ListenPort = int.Parse(parts[0]);
                        TargetApiUrl = $"http://{parts[1]}:{parts[2]}/{parts[3]}";
                    }
                }
            } catch { Console.WriteLine("[Config] Error loading gateway-settings.csv"); }

            // 2. 장비 인벤토리 로드
            try {
                if (File.Exists("device-inventory.sample.csv")) {
                    var devLines = File.ReadAllLines("device-inventory.sample.csv");
                    foreach (var line in devLines) {
                        if (line.StartsWith("#") || string.IsNullOrWhiteSpace(line)) continue;
                        var parts = line.Split(',');
                        if (parts.Length >= 4) DeviceMap[parts[0]] = parts[3]; // DeviceID -> SiteCode 매핑
                    }
                }
            } catch { Console.WriteLine("[Config] Error loading device-inventory.csv"); }
        }
    }
}