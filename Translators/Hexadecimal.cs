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
        private static readonly int MaxHexLength = 30;

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
        /// <returns>The decimal integer equivalent of the hexadecimal string.</returns>
        /// <exception cref="ArgumentException">Thrown if the input string is not a valid hexadecimal number or is too large.</exception>
        public static int TranslateFrom(string hexadecimalNum)
        {
            if (!IsValid(hexadecimalNum))
            {
                throw new ArgumentException($"Number {hexadecimalNum} is not valid hexadecimal number!");
            }

            if (hexadecimalNum.Length > MaxHexLength)
            {
                throw new ArgumentException($"Number {hexadecimalNum} is too large!");
            }

            int decimalOutput = 0;
            int hexLength = hexadecimalNum.Length;

            for (int i = 0; i < hexLength; i++)
            {
                char hexChar = char.ToUpper(hexadecimalNum[i]);

                if (!HexValues.TryGetValue(hexChar, out int hexValue))
                {
                    throw new Exception($"Unexpected error while converting {hexChar} into integer representative.");
                }
                decimalOutput += hexValue * (int)Math.Pow(16, hexLength - i - 1);
            }

            return decimalOutput;
        }

        /// <summary>
        /// Converts a decimal integer to a hexadecimal string representation.
        /// </summary>
        /// <param name="decimalNum">The decimal integer to convert.</param>
        /// <returns>The hexadecimal string representation of the decimal integer.</returns>
        public static string TranslateTo(int decimalNum)
        {
            StringBuilder hexOutput = new();
            do
            {
                int remainder = decimalNum % 16;

                if (remainder < 10)
                {
                    // insert remainder as digit into StringBuilder
                    hexOutput.Insert(0, remainder);
                }
                else
                {
                    // convert to corresponding hexadecimal character and insert into StringBuilder
                    char hexChar = (char)(remainder - 10 + 'A'); // A is 10 in decimal
                    hexOutput.Insert(0, hexChar);
                }

                decimalNum /= 16;
            } while (decimalNum > 0);

            return hexOutput.ToString();
        }

        /// <summary>
        /// Check if a string hexadecimal number is a valid via custom rules.
        /// </summary>
        /// <param name="hexNumber">A string representing a hexadecimal number</param>
        /// <returns>True if the hexadecimal number is a valid hexadecimal number, false otherwise</returns>
        private static bool IsValid(string hexNumber)
        {
            foreach (char item in hexNumber)
            {
                if (!HexValues.ContainsKey(char.ToUpper(item)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}