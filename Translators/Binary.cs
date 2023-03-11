namespace NumberConversion.Translators
{
    public static class Binary
    {
        public static int TranslateFrom(string binaryNumRaw)
        {
            if (!IsValid(binaryNumRaw))
            {
                throw new ArgumentException($"Number {binaryNumRaw} is not valid binary number!");
            }

            int binaryNum = Convert.ToInt32(binaryNumRaw);
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

            if (decimalNum < 0)
            {
                decimalNum *= -1;
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
        /// <param name="binaryNumber">The string to check.</param>
        /// <returns>True if the input is a valid binary number, false otherwise.</returns>
        private static bool IsValid(string binaryNumber)
        {
            if (!int.TryParse(binaryNumber, out int _))
            {
                return false;
            }

            if (!double.TryParse(binaryNumber, out double _))
            {
                return false;
            }

            foreach (char c in binaryNumber)
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
