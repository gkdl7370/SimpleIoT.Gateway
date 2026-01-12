using System.Runtime.Serialization;

namespace SimpleIoT.Gateway.Models
{
    [DataContract]
    public class TelemetryData
    {
        [DataMember(Name = "site_code")]
        public string SiteCode { get; set; } = string.Empty;

        [DataMember(Name = "device_id")]
        public string DeviceId { get; set; } = string.Empty;

        [DataMember(Name = "measured_at")]
        public string MeasuredAt { get; set; } = string.Empty;

        [DataMember(Name = "value_x")]
        public float ValueX { get; set; }

        [DataMember(Name = "value_y")]
        public float ValueY { get; set; }

        [DataMember(Name = "battery")]
        public float Battery { get; set; }

        public string GetUniqueKey() => $"{SiteCode}_{DeviceId}_{MeasuredAt}";
    }
}