using System.Text;
using System.Text.RegularExpressions;

namespace NumberConversion.Translators
{
    /// <summary>
    /// Provides methods for converting Roman numerals to and from decimal numbers.
    /// </summary>
    public static partial class Roman
    {
        /// <summary>
        /// The maximum decimal value that can be converted to a Roman numeral.
        /// </summary>
        private static readonly int MaxInputValue = 3999;


        /// <summary>
        /// A dictionary mapping Roman numerals to their decimal values.
        /// </summary>
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

        /// <summary>
        /// A dictionary mapping decimal values to their Roman numeral representations.
        /// </summary>
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
        public static long TranslateFrom(string romanNum)
        {
            long decimalOutput = 0;

            if (IsValid(romanNum))
            {
                long previous = 0;
                foreach (char currentRoman in romanNum)
                {
                    long current = RomanToDecimalDictionary[currentRoman];
                    if (previous < current)
                    {
                        decimalOutput -= previous;
                        current -= previous;
                    }
                    decimalOutput += current;
                    previous = current;
                }
            }
            return decimalOutput;
        }

        /// <summary>
        /// Converts a decimal number to its Roman numeral representation.
        /// </summary>
        /// <param name="decimalNum">The decimal number to convert.</param>
        /// <returns>The Roman numeral representation of the input decimal number.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the decimal number is zero, which is not defined in the Roman numeral system.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the decimal number exceeds the maximum allowed value for conversion to Roman numeral.</exception>
        public static string TranslateTo(long decimalNum)
        {
            StringBuilder romanOutput = new();

            if (decimalNum == 0)
            {
                throw new InvalidOperationException("Zero is not defined as character in Roman system.");
            }

            if (decimalNum > MaxInputValue)
            {
                throw new ArgumentOutOfRangeException($"Value '{decimalNum}' exceeds maximum allowed value for conversion into Roman number.");
            }

            foreach (KeyValuePair<int, string> row in DecimalToRomanDictionary)
            {
                while (decimalNum >= row.Key)
                {
                    romanOutput.Append(row.Value);
                    decimalNum -= row.Key;
                }
            }

            return romanOutput.ToString();
        }

        [GeneratedRegex("^M*(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$")]
        private static partial Regex RomanRegex();
        private static bool IsValid(string romanNumber)
        {
            if (string.IsNullOrEmpty(romanNumber))
            {
                throw new ArgumentNullException(nameof(romanNumber), $"Variable '{nameof(romanNumber)}' is possible null reference.");
            }

            if (!RomanRegex().IsMatch(romanNumber))
            {
                throw new ArgumentException($"Value '{romanNumber}' is not valid Roman number.");
            }

            return true;
        }
    }
}