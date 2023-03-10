namespace NumberConversion.Translators
{
    static class Binary
    {
        public static int TranslateFrom(int binaryNum)
        {
            if (!IsValid(binaryNum.ToString()))
            {
                throw new ArgumentException($"Number {binaryNum} is not valid binary number!");
            }

            int decimalOutput = 0;
            int base_ = 1;
            while (binaryNum > 0)
            {
                int reminder = binaryNum % 10;
                binaryNum /= 10;
                decimalOutput += reminder * base_;
                base_ *= 2;
            }

            return decimalOutput;
        }

        public static string TranslateTo(int decimalNum)
        {
            if (decimalNum == 0)
            {
                return "0";
            }

            string binaryOutput = string.Empty;
            while (decimalNum > 0)
            {
                int remainder = decimalNum % 2;
                decimalNum /= 2;
                binaryOutput = remainder.ToString() + binaryOutput;
            }

            return binaryOutput;
        }

        /// <summary>
        /// Checks whether a given string input is a valid binary number.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <returns>True if the input is a valid binary number, false otherwise.</returns>
        public static bool IsValid(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            foreach (char c in input)
            {
                if (c != '0' && c != '1')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
