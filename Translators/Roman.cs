using System.Text;

namespace NumberConversion.Translators
{
    /// <summary>
    /// Provides methods for converting Roman numerals to and from decimal numbers.
    /// </summary>
    public static class Roman
    {
        private static readonly Dictionary<char, int> RomanToDecimalDictionary = new()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 },
        };

        private static readonly Dictionary<int, string> DecimalToRomanDictionary = new()
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" },
        };

        /// <summary>
        /// Converts a Roman numeral to its decimal representation.
        /// </summary>
        /// <param name="roman">The Roman numeral to convert.</param>
        /// <returns>The decimal representation of the input Roman numeral.</returns>
        public static int TranslateFrom(string roman)
        {
            int decimalOutput = 0;

            int previous = 0;
            foreach (char currentRoman in roman)
            {
                int current = RomanToDecimalDictionary[currentRoman];
                if (previous != 0 && current > previous)
                {
                    decimalOutput -= (2 * previous) + current;
                }
                else
                {
                    decimalOutput += current;
                }

                previous = current;
            }

            return decimalOutput;
        }

        /// <summary>
        /// Converts a decimal number to its Roman numeral representation.
        /// </summary>
        /// <param name="number">The decimal number to convert.</param>
        /// <returns>The Roman numeral representation of the input decimal number.</returns>
        public static string TranslateTo(int number)
        {
            if (number == 0)
            {
                return "nulla";
            }

            StringBuilder romanOutput = new();
            foreach (var row in DecimalToRomanDictionary)
            {
                while (number >= row.Key)
                {
                    romanOutput.Append(row.Value);
                    number -= row.Key;
                }
            }

            return romanOutput.ToString();
        }
    }
}