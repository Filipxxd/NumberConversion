using System.Text;

namespace NumberConversion.Translators
{
    public static class Octal
    {
        /// <summary>
        /// Converts an octal string to a decimal integer.
        /// </summary>
        /// <param name="octalNumRaw">The octal string to convert. The input string must be a valid octal number.</param>
        /// <returns>The decimal Int64 equivalent of the octal string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input string is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown if the input string is not a valid octal number or is too large.</exception>
        /// <exception cref="OverflowException">Thrown if the conversion result exceeds the maximum value for a 64-bit integer.</exception>
        public static long TranslateFrom(string octalNumRaw)
        {
            long decimalOutput = 0;

            if (IsValid(octalNumRaw))
            {
                long octalNum = Convert.ToInt64(octalNumRaw);
                long multiplier = 1;
                while (octalNum > 0)
                {
                    checked
                    {
                        long previous = decimalOutput;
                        long last_digit = octalNum % 10;
                        octalNum /= 10;
                        decimalOutput += last_digit * multiplier;
                        multiplier *= 8;

                        if (decimalOutput < previous)
                        {
                            throw new OverflowException($"Value '{octalNumRaw}' exceeded Int64 maximum value while trying to convert into decimal system.");
                        }
                    }
                }
            }
            return decimalOutput;
        }

        /// <summary>
        /// Converts a decimal integer to an octal string representation.
        /// </summary>
        /// <param name="decimalNum">The decimal integer to convert.</param>
        /// <returns>The octal string representation of the decimal integer.</returns>
        public static string TranslateTo(long decimalNum)
        {
            if (decimalNum == 0)
            {
                return "0";
            }

            StringBuilder octalOutput = new();
            for (; decimalNum > 0; decimalNum /= 8)
            {
                octalOutput.Insert(0, decimalNum % 8);
            }

            return octalOutput.ToString();
        }

        /// <summary>
        /// Checks if a string representing an octal number is valid according to custom rules.
        /// </summary>
        /// <param name="octalNumber">The string to check.</param>
        /// <returns>True if the input string is a valid hexadecimal number, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the input string is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the input string exceeds the maximum allowed length for conversion.</exception>
        /// <exception cref="ArgumentException">Thrown if the input string is not a valid hexadecimal number.</exception>
        private static bool IsValid(string octalNumber)
        {
            if (string.IsNullOrEmpty(octalNumber))
            {
                throw new ArgumentNullException(nameof(octalNumber), $"Variable '{nameof(octalNumber)}' is possible null reference.");
            }

            if (!double.TryParse(octalNumber, out double _))
            {
                throw new ArgumentException($"Number '{octalNumber}' is not valid octal number!");
            }

            foreach (char c in octalNumber)
            {
                if (c == '9' || c == '8')
                {
                    throw new ArgumentException($"Number '{octalNumber}' is not valid octal number!");
                }
            }

            return true;
        }
    }
}
