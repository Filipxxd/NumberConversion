using System.Text;

namespace NumberConversion.Translators
{
    /// <summary>
    /// Provides methods for converting between hexadecimal and decimal numbers.
    /// </summary>
    public static class Hexadecimal
    {
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
        /// <param name="hexadecimalNum">The hexadecimal string to convert.</param>
        /// <returns>The decimal integer equivalent of the hexadecimal string.</returns>
        /// <exception cref="ArgumentException">Thrown if the input string is not a valid hexadecimal string.</exception>
        public static int TranslateFrom(string hexadecimalNum)
        {
            hexadecimalNum = hexadecimalNum.ToUpper();
            if (!IsValid(hexadecimalNum))
            {
                throw new ArgumentException($"Number {hexadecimalNum} is not valid hexadecimal number!");
            }

            int decimalOutput = 0;
            int hexLength = hexadecimalNum.Length;

            for (int i = 0; i < hexLength; i++)
            {
                char hexChar = hexadecimalNum[i];

                if (HexValues.TryGetValue(hexChar, out int hexValue))
                {
                    decimalOutput += hexValue * (int)Math.Pow(16, hexLength - i - 1);
                }
                else
                {
                    throw new ArgumentException($"Number {hexadecimalNum} is not valid hexadecimal number!");
                }
            }

            return decimalOutput;
        }

        /// <summary>
        /// Converts a decimal integer to a hexadecimal string.
        /// </summary>
        /// <param name="decimalNum">The decimal integer to convert.</param>
        /// <returns>The hexadecimal string equivalent of the decimal integer.</returns>
        public static string TranslateTo(int decimalNum)
        {
            if (decimalNum == 0)
            {
                return "0";
            }

            StringBuilder hexOutput = new();

            do
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
            } while (decimalNum > 0);

            return hexOutput.ToString();
        }

        /// <summary>
        /// Check if a string hexadecimal number is a valid hexadecimal number.
        /// </summary>
        /// <param name="hexNumber">A string representing a hexadecimal number</param>
        /// <returns>True if the hexadecimal number is a valid hexadecimal number, false otherwise</returns>
        private static bool IsValid(string hexNumber)
        {
            foreach (char item in hexNumber)
            {
                if (!HexValues.ContainsKey(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}