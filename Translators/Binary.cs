namespace NumberConversion.Translators
{
    public static class Binary
    {
        public static int TranslateFrom(string binaryNumRaw)
        {
            int decimalOutput = 0;
            if (IsValidBinary(binaryNumRaw))
            {
                for (int i = 0; i < binaryNumRaw.Length; i++)
                {
                    checked
                    {
                        int digit = binaryNumRaw[binaryNumRaw.Length - 1 - i] - '0';
                        int previous = decimalOutput;
                        decimalOutput += digit * (int)Math.Pow(2, i);
                        if (decimalOutput < previous)
                        {
                            throw new OverflowException();
                        }
                    }

                }
            }
            return decimalOutput;
        }


        public static string TranslateTo(int decimalNum)
        {
            string binaryOutput = string.Empty;
            if (IsValidDecimal(decimalNum))
            {
                if (decimalNum == 0)
                {
                    return "0";
                }

                if (decimalNum < 0)
                {
                    decimalNum *= -1;
                }

                while (decimalNum > 0)
                {
                    int remainder = decimalNum % 2;
                    decimalNum /= 2;
                    binaryOutput = remainder.ToString() + binaryOutput;
                }
            }
            return binaryOutput;
        }

        private static bool IsValidDecimal(int decimalNum)
        {
            if (!int.TryParse(decimalNum.ToString(), out int _))
            {
                throw new ArgumentException();
            }
            return true;
        }

        /// <summary>
        /// Checks whether a given string input is a valid binary number.
        /// </summary>
        /// <param name="binaryNumber">The string to check.</param>
        /// <returns>True if the input is a valid binary number, false otherwise.</returns>
        private static bool IsValidBinary(string binaryNumber)
        {
            if (string.IsNullOrEmpty(binaryNumber))
            {
                throw new ArgumentNullException($"Number {binaryNumber} is not valid binary number!");
            }

            foreach (char c in binaryNumber)
            {
                if (c != '0' && c != '1')
                {
                    throw new ArgumentException();
                }
            }

            return true;
        }
    }
}
