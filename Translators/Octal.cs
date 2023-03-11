namespace NumberConversion.Translators
{
    static class Octal
    {
        public static int TranslateFrom(string octalNumRaw)
        {
            if (!IsValid(octalNumRaw))
            {
                throw new ArgumentException($"Number {octalNumRaw} is not valid octal number!");
            }

            int octalNum = Convert.ToInt32(octalNumRaw);
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

        public static bool IsValid(string octalNumber)
        {
            if (!int.TryParse(octalNumber, out int _))
            {
                return false;
            }

            foreach (char c in octalNumber)
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
