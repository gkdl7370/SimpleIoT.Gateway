using SimpleIoT.Gateway.Utils;
using Xunit;

namespace SimpleIoT.Gateway.Tests;

public class DataParserTests
{
    [Fact]
    [Trait("설명", "ASCII 바이트 배열에서 지정한 구간을 읽고 공백을 제거한다")]
    public void ByteArrayToASCIIReturnsTrimmedAsciiSegment()
    {
        byte[] payload = "STXDEVICE-001   TAIL"u8.ToArray();

        string result = DataParser.ByteArrayToASCII(payload, 3, 13);

        Assert.Equal("DEVICE-001", result);
    }

    [Fact]
    [Trait("설명", "잘못된 구간을 요청하면 빈 문자열을 반환한다")]
    public void ByteArrayToASCIIReturnsEmptyStringWhenRangeIsInvalid()
    {
        byte[] payload = "short"u8.ToArray();

        string result = DataParser.ByteArrayToASCII(payload, 2, 20);

        Assert.Equal(string.Empty, result);
    }

    [Theory]
    [Trait("설명", "센서 타입 코드를 사람이 읽을 수 있는 이름으로 변환한다")]
    [InlineData(0x01, "Temperature/Humidity")]
    [InlineData(0x10, "Water Level")]
    [InlineData(0x7F, "Unknown Device")]
    public void GetSensorTypeDisplayNameMapsKnownCodes(byte code, string expected)
    {
        Assert.Equal(expected, DataParser.GetSensorTypeDisplayName(code));
    }
}
