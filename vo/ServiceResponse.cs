using System.Runtime.Serialization;

namespace SimpleIoT.Gateway.Models
{
    [DataContract]
    public class ServiceResponse<T> 
    {
        [DataMember(Name = "status")]
        public string Status { get; set; } = string.Empty;

        [DataMember(Name = "message")]
        public string SuccessMessage { get; set; } = string.Empty;

        [DataMember(Name = "error")]
        public string ErrorMessage { get; set; } = string.Empty;

        [DataMember(Name = "data")]
        public T? Data { get; set; } // 데이터는 없을 수 있으므로 T?
    }
}