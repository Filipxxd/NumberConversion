namespace NumberConversion.Translators
{
    static class Octal
    {
        public static int TranslateFrom(int octalNum)
        {
            if (!IsValid(octalNum))
            {
                throw new ArgumentException($"Number {octalNum} is not valid octal number!");
            }

            int decimalOutput = 0;
            int exponent = 1;
            while (octalNum > 0)
            {
                int last_digit = octalNum % 10;
                octalNum /= 10;
                decimalOutput += last_digit * exponent;
                exponent *= 8;
            }
            return decimalOutput;
        }

        public static string TranslateTo(int decimalNum)
        {
            if (decimalNum == 0)
            {
                return "0";
            }

            string octalOutput = string.Empty;
            int i = 0;
            while (decimalNum != 0)
            {
                octalOutput = (decimalNum % 8).ToString() + octalOutput;
                decimalNum /= 8;
                i++;
            }
            return octalOutput;
        }

        public static bool IsValid(int input)
        {
            if (string.IsNullOrEmpty(input.ToString()) && input != 0)
            {
                return false;
            }

            foreach (char c in input.ToString())
            {
                if (c == '9')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
