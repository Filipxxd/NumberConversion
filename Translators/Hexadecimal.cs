using System.Text;

namespace NumberConversion.Translators
{
    /// <summary>
    /// Provides methods for converting between hexadecimal and decimal numbers.
    /// </summary>
    public static class Hexadecimal
    {
        /// <summary>
        /// A dictionary that maps hexadecimal characters to their integer values.
        /// </summary>
        private static readonly Dictionary<char, int> HexValues = new()
        {
            { '0', 0 },
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'A', 10 },
            { 'B', 11 },
            { 'C', 12 },
            { 'D', 13 },
            { 'E', 14 },
            { 'F', 15 }
        };

        /// <summary>
        /// Converts a hexadecimal string to a decimal integer.
        /// </summary>
        /// <param name="hexadecimalNum">The hexadecimal string to convert. The input string must be a valid hexadecimal number.</param>
        /// <returns>The decimal Int32 equivalent of the hexadecimal string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input string is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown if the input string is not a valid hexadecimal number or is too large.</exception>
        /// <exception cref="OverflowException">Thrown if the conversion result exceeds the maximum value for a 32-bit integer.</exception>
        public static long TranslateFrom(string hexadecimalNum)
        {
            long decimalOutput = 0;

            if (hexadecimalNum.StartsWith("0x"))
            {
                hexadecimalNum = hexadecimalNum[2..];
            }

            if (IsValid(hexadecimalNum))
            {
                for (int i = 0; i < hexadecimalNum.Length; i++)
                {
                    char hexChar = char.ToUpper(hexadecimalNum[i]);
                    int hexValue = HexValues[hexChar];

                    checked
                    {
                        long previousValue = decimalOutput;
                        decimalOutput = decimalOutput * 16 + hexValue;
                        if (previousValue > decimalOutput)
                        {
                            throw new OverflowException($"Value '{hexadecimalNum}' exceeded Int64 maximum value while trying to convert into decimal system.");
                        }
                    }
                }
            }
            return decimalOutput;
        }


        /// <summary>
        /// Converts a decimal integer to a hexadecimal string representation.
        /// </summary>
        /// <param name="decimalNum">The decimal integer to convert.</param>
        /// <returns>The hexadecimal string representation of the decimal integer.</returns>
        /// <exception cref="ArgumentException">Thrown if the input integer is not a valid 32-bit integer.</exception>
        public static string TranslateTo(long decimalNum)
        {
            if (decimalNum == 0)
            {
                return "0";
            }

            StringBuilder hexOutput = new();
            while (decimalNum > 0)
            {
                long remainder = decimalNum % 16;
                if (remainder < 10)
                {
                    hexOutput.Insert(0, remainder);
                }
                else
                {
                    char hexChar = (char)(remainder - 10 + 'A'); // ASCII 65
                    hexOutput.Insert(0, hexChar);
                }
                decimalNum /= 16;
            }

            return hexOutput.ToString();
        }

        /// <summary>
        /// Checks if a string representing a hexadecimal number is valid according to custom rules.
        /// </summary>
        /// <param name="hexadecimalNum">The string to check.</param>
        /// <returns>True if the input string is a valid hexadecimal number, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input string is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the input string exceeds the maximum allowed length for conversion.</exception>
        /// <exception cref="ArgumentException">Thrown if the input string is not a valid hexadecimal number.</exception>
        private static bool IsValid(string hexadecimalNum)
        {
            if (string.IsNullOrEmpty(hexadecimalNum))
            {
                throw new ArgumentNullException(nameof(hexadecimalNum), $"Variable '{nameof(hexadecimalNum)}' is possible null reference.");
            }

            foreach (char item in hexadecimalNum)
            {
                if (!HexValues.ContainsKey(char.ToUpper(item)))
                {
                    throw new ArgumentException($"Value '{hexadecimalNum}' is not a valid hexadecimal number!");
                }
            }

            return true;
        }
    }
}
