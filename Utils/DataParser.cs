using System;
using System.Text;

namespace SimpleIoT.Gateway.Utils
{
    /// <summary>
    /// 바이너리 데이터를 분석하여 유의미한 정보로 변환하는 유틸리티입니다.
    /// </summary>
    public static class DataParser
    {
        #region 데이터 해석 로직
        
        public static string ToYearString(int val) => (val + 2000).ToString();

        public static string ToDoubleDigitString(int val) => val < 10 ? $"0{val}" : val.ToString();

        public static string ByteArrayToAscii(byte[] bytes, int startIdx, int length)
        {
            return Encoding.ASCII.GetString(bytes, startIdx, length).Trim();
        }

        public static string ByteToHex(byte _byte) => $"0x{_byte:X2}";

        #endregion

        #region 장비 및 지역 매핑 (익명화 완료)

        public static string GetSensorTypeDisplayName(byte typeCode)
        {
            return typeCode switch
            {
                0x01 => "Temperature/Humidity",
                0x05 => "Vibration",
                0x10 => "Water Level",
                0x11 => "Multi-Sensor",
                0x12 => "Flow Meter",
                _ => "Unknown Device"
            };
        }

        public static string GetLocationCategoryName(byte siteCode)
        {
            return siteCode switch
            {
                0x00 => "River/Stream",
                0x01 => "Inland Area",
                0x02 => "Coastal Area",
                0x03 => "Reservoir",
                0x04 => "Steep Slope",
                _ => "General Site"
            };
        }

        #endregion

        #region 바이트 변환 함수
        public static byte[] ToBytes(ushort val) => BitConverter.GetBytes(val);
        public static byte[] ToBytes(short val) => BitConverter.GetBytes(val);
        public static byte[] ToBytes(uint val) => BitConverter.GetBytes(val);
        #endregion



        // 3. 메서드 이름 대소문자 주의 (ByteArrayToASCII)
        public static string ByteArrayToASCII(byte[] bytes, int startIdx, int length)
        {
            try
            {
                if (bytes == null || startIdx + length > bytes.Length)
                    return string.Empty;

                return Encoding.ASCII.GetString(bytes, startIdx, length).Trim('\0', ' ');
            }
            catch
            {
                return string.Empty;
            }
        }
        
    }
}