using System;
using System.Numerics;

namespace EthTxSearch
{
    public static class HexExtensions
    {
        private const int WeiToEthDecPlaces = 18;

        /// <summary>
        /// Returns a uint for a hex string.
        /// Not case sensitive.
        /// Handles zero padding start.
        /// </summary>
        public static uint HexToUint(this string hexString)
        {
            if (!hexString.StartsWith("0x"))
                throw new InvalidOperationException("Only strings prefaced with 0x may be decoded from hex in this iteration.");

            return Convert.ToUInt32(hexString.Substring(2), 16);
        }

        /// <summary>
        /// Returns a byte array from a hex string.
        /// Not case sensitive.
        /// Handles zero padding start.
        /// </summary>
        public static byte[] HexToByteArray(this string hexString)
        {
            if (!hexString.StartsWith("0x"))
                throw new InvalidOperationException("Only strings prefaced with 0x may be decoded from hex in this iteration.");

            string toHex = hexString.Substring(2);

            // Pad zero values
            if (toHex.Length % 2 != 0)
                toHex = toHex.Insert(0, "0");

            int numberChars = toHex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(toHex.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// Returns a string representation of a value in Ethereum.
        /// This is super dirty but for our viewing-only purposes it's a simple approach.
        /// Not case sensitive.
        /// Handles zero padding start.
        /// </summary>
        public static string HexToEthValue(this string hexString)
        {
            if (hexString == "0x0")
                return "0";

            byte[] bytes = hexString.HexToByteArray();
            BigInteger bigInt = new BigInteger(bytes, true, true);
            string stringRep = bigInt.ToString();

            // Insert decimal place
            if (stringRep.Length > WeiToEthDecPlaces)
            {
                int decPlacePosition = stringRep.Length - WeiToEthDecPlaces;
                stringRep = stringRep.Insert(decPlacePosition, ".");
            }
            else
            {
                int zerosToAdd = WeiToEthDecPlaces - stringRep.Length;
                stringRep = stringRep.Insert(0, new string('0', zerosToAdd));
                stringRep = stringRep.Insert(0, "0.");
            }

            // Remove trailing zeros
            stringRep = stringRep.TrimEnd('0');
            stringRep = stringRep.TrimEnd('.');

            return stringRep;
        }

        public static string UIntToHexString(this uint val)
        {
            return "0x" + val.ToString("X");
        }

        public static bool IsValidHex(this string chars)
        {
            if (!chars.StartsWith("0x"))
                return false;

            bool isHex;
            foreach (var c in chars.Substring(2))
            {
                isHex = ((c >= '0' && c <= '9') ||
                         (c >= 'a' && c <= 'f') ||
                         (c >= 'A' && c <= 'F'));

                if (!isHex)
                    return false;
            }
            return true;
        }
    }
}
