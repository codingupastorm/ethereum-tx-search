using Xunit;

namespace EthTxSearch.Tests
{
    public class HexConversionTests
    {
        [Theory]
        [InlineData("0x0", 0)]
        [InlineData("0x249f0", 150000)]
        public void HexToUint(string input, uint expected)
        {
            uint converted = input.HexToUint();
            Assert.Equal(expected, converted);
        }

        [Theory]
        [InlineData("0x0", new byte[] { 0 })]
        [InlineData("0x1", new byte[] { 1 })]
        [InlineData("0x10", new byte[] { 16 })]
        [InlineData("0x0123456789ABCDEF", new byte[] { 1, 35, 69, 103, 137, 171, 205, 239 })]
        public void HexToByteArray(string input, byte[] expected)
        {
            byte[] converted = input.HexToByteArray();
            Assert.Equal(expected, converted);
        }

        [Theory]
        [InlineData("0x0", "0")]
        [InlineData("0x1aa535d3d0c000", "0.0075")]
        [InlineData("0xDE0B6B3A7640000", "1")]
        public void HexToEthValue(string input, string expected)
        {
            string converted = input.HexToEthValue();
            Assert.Equal(expected, converted);
        }
    }
}
