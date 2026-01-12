namespace SimpleIoT.Gateway.Models
{
    public class SensorPacket
    {
        public string ClientIp { get; set; } = string.Empty;
        public string StartToken { get; set; } = string.Empty;
        public string MessageId { get; set; } = string.Empty;
        public string CategoryType { get; set; } = string.Empty;
        public int PayloadLength { get; set; }
        public string SiteCode { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public int SamplingInterval { get; set; }
        public float ValueX { get; set; }
        public float ValueY { get; set; }
        public float ValueZ { get; set; }
        public float ExtraValue { get; set; }
        public int Distance { get; set; }
        public string EndToken { get; set; } = string.Empty;
        public byte Checksum { get; set; }
        public int IntensityX { get; set; }
        public int IntensityY { get; set; }
        public int IntensityZ { get; set; }
        public string TerminationFlag { get; set; } = string.Empty;
    }
}