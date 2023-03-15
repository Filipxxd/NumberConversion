using System.Text;

namespace NumberConversion.Translators
{
    /// <summary>
    /// Provides methods to translate numbers between binary and decimal systems.
    /// </summary>
    public static class Binary
    {
        /// <summary>
        /// Translates a binary number represented as a string to a decimal number.
        /// </summary>
        /// <param name="binaryNumRaw">The binary number to translate.</param>
        /// <returns>The decimal equivalent of the binary number.</returns>
        public static long TranslateFrom(string binaryNum)
        {
            long decimalOutput = 0;
            if (IsValid(binaryNum))
            {
                for (int i = 0; i < binaryNum.Length; i++)
                {
                    checked
                    {
                        long digit = binaryNum[binaryNum.Length - 1 - i] - '0'; // ASCII 48
                        long previous = decimalOutput;
                        decimalOutput += digit * (int)Math.Pow(2, i);
                        if (decimalOutput < previous)
                        {
                            throw new OverflowException($"Value '{binaryNum}' exceeded Int64 maximum value while trying to convert into decimal system.");
                        }
                    }
                }
            }
            return decimalOutput;
        }

        /// <summary>
        /// Converts a given decimal number to binary representation.
        /// </summary>
        /// <param name="decimalNum">The decimal number to convert.</param>
        /// <returns>A string representing the binary representation of the input number.</returns>
        public static string TranslateTo(long decimalNum)
        {
            if (decimalNum == 0)
            {
                return "0";
            }

            StringBuilder binaryOutput = new();
            long quotient = decimalNum;
            while (quotient > 0)
            {
                long remainder = quotient % 2;
                quotient /= 2;
                binaryOutput.Insert(0, remainder);
            }

            return binaryOutput.ToString();
        }

        /// <summary>
        /// Checks whether a given string input is a valid binary number.
        /// </summary>
        /// <param name="binaryNumber">The string to check.</param>
        /// <returns>True if the input is a valid binary number, false otherwise.</returns>
        private static bool IsValid(string binaryNumber)
        {
            if (string.IsNullOrEmpty(binaryNumber))
            {
                throw new ArgumentNullException(nameof(binaryNumber), $"Variable '{nameof(binaryNumber)}' is possible null reference.");
            }

            foreach (char c in binaryNumber)
            {
                if (c != '0' && c != '1')
                {
                    throw new ArgumentException($"Value '{binaryNumber}' is not valid binary number!");
                }
            }
            return true;
        }
    }
}
