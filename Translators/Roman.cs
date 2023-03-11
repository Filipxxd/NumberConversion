using System.Text;
using System.Text.RegularExpressions;

namespace NumberConversion.Translators
{
    /// <summary>
    /// Provides methods for converting Roman numerals to and from decimal numbers.
    /// </summary>
    public static class Roman
    {
        private static readonly int MaxInputValue = 20000; // In decimal

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
        /// <param name="romanNum">The Roman numeral to convert.</param>
        /// <returns>The decimal representation of the input Roman numeral.</returns>
        public static int TranslateFrom(string romanNum)
        {
            if (romanNum == "0")
            {
                throw new InvalidOperationException($"Zero is not defined in Roman Numerical System!");
            }

            if (!IsValid(romanNum))
            {
                throw new ArgumentException($"Number {romanNum} is not valid roman number!");
            }


            int decimalOutput = 0;
            int previous = 0;
            foreach (char currentRoman in romanNum)
            {
                int current = RomanToDecimalDictionary[currentRoman];
                if (previous < current)
                {
                    decimalOutput -= previous;
                    current -= previous;
                }
                decimalOutput += current;
                previous = current;
            }

            return decimalOutput;
        }

        /// <summary>
        /// Converts a decimal number to its Roman numeral representation.
        /// </summary>
        /// <param name="decimalNum">The decimal number to convert.</param>
        /// <returns>The Roman numeral representation of the input decimal number.</returns>
        public static string TranslateTo(int decimalNum)
        {
            if (decimalNum == 0)
            {
                return "NON-DEFINED";
            }

            if (decimalNum > MaxInputValue)
            {
                throw new ArgumentOutOfRangeException($"Limit for converting to Roman is {MaxInputValue}!");
            }

            StringBuilder romanOutput = new();
            foreach (var row in DecimalToRomanDictionary)
            {
                while (decimalNum >= row.Key)
                {
                    romanOutput.Append(row.Value);
                    decimalNum -= row.Key;
                }
            }

            return romanOutput.ToString();
        }

        public static bool IsValid(string romanNumber)
        {
            Regex pattern = new("^M*(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$");


            if (!pattern.IsMatch(romanNumber))
            {
                return false;
            }

            return true;
        }
    }
}