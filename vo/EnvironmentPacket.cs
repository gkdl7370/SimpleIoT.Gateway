namespace SimpleIoT.Gateway.Models
{
    public class EnvironmentPacket : SensorPacket
    {
        public ushort MeasuredValue { get; set; }
        public ushort BatteryLevel { get; set; }
        public string SequenceNo { get; set; } = string.Empty;
    }
}