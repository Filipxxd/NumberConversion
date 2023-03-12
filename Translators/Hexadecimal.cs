using System.Text;

namespace NumberConversion.Translators
{
    /// <summary>
    /// Provides methods for converting between hexadecimal and decimal numbers.
    /// </summary>
    public static class Hexadecimal
    {
        /// <summary>
        /// The maximum allowed length of a valid hexadecimal string.
        /// </summary>
        private const int MaxHexLength = 10;

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
        public static int TranslateFrom(string hexadecimalNum)
        {
            if (string.IsNullOrEmpty(hexadecimalNum))
            {
                throw new ArgumentNullException(nameof(hexadecimalNum));
            }

            bool isNegative = false;
            if (hexadecimalNum[0] == '-')
            {
                isNegative = true;
                hexadecimalNum = hexadecimalNum[1..];
            }

            if (!IsValidHexadecimal(hexadecimalNum))
            {
                throw new ArgumentException($"Value {hexadecimalNum} is not a valid hexadecimal number!");
            }

            int decimalOutput = 0;
            for (int i = 0; i < hexadecimalNum.Length; i++)
            {
                char hexChar = char.ToUpper(hexadecimalNum[i]);

                if (!HexValues.TryGetValue(hexChar, out int hexValue))
                {
                    throw new Exception($"Unexpected error while converting {hexChar} into integer representative.");
                }

                checked
                {
                    int previous = decimalOutput;
                    decimalOutput = decimalOutput * 16 + hexValue;

                    if (decimalOutput < previous)
                    {
                        throw new OverflowException();
                    }
                }
            }

            return isNegative ? -decimalOutput : decimalOutput;
        }

        /// <summary>
        /// Converts a decimal integer to a hexadecimal string representation.
        /// </summary>
        /// <param name="decimalNum">The decimal integer to convert.</param>
        /// <returns>The hexadecimal string representation of the decimal integer.</returns>
        /// <exception cref="ArgumentException">Thrown if the input integer is not a valid 32-bit integer.</exception>
        public static string TranslateTo(int decimalNum)
        {
            if (!IsValidDecimal(decimalNum))
            {
                throw new ArgumentException($"Value {decimalNum} is not a valid integer!");
            }

            if (decimalNum == 0)
            {
                return "0";
            }

            StringBuilder hexOutput = new();
            while (decimalNum > 0)
            {
                int remainder = decimalNum % 16;
                if (remainder < 10)
                {
                    hexOutput.Insert(0, remainder);
                }
                else
                {
                    char hexChar = (char)(remainder - 10 + 'A');
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
        private static bool IsValidHexadecimal(string hexadecimalNum)
        {
            if (hexadecimalNum.Length > MaxHexLength)
            {
                throw new ArgumentOutOfRangeException($"Value {hexadecimalNum} exceeds maximum allowed length for conversion.");
            }

            foreach (char item in hexadecimalNum)
            {
                if (!HexValues.ContainsKey(char.ToUpper(item)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if an integer is a valid 32-bit integer.
        /// </summary>
        /// <param name="decimalNum">The integer to check.</param>
        /// <returns>True if the input integer is a valid 32-bit integer, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if the input integer is not a valid 32-bit integer.</exception>
        private static bool IsValidDecimal(int decimalNum)
        {
            return decimalNum >= 0;
        }
    }
}
