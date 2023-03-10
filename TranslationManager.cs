using NumberConversion.Translators;

namespace NumberConversion
{
    static class TranslationManager
    {
        private readonly static int MaxValueForConversion = 10000;

        public static List<string> NumericalSystems = new() { "Binary", "Roman", "Decimal", "Hexadecimal", "Octal" };

        /// <summary>
        /// Converts a decimal value to a list of pairs: number system and value.
        /// </summary>
        /// <param name="value">The decimal value to convert.</param>
        /// <param name="systems">The list of number systems to convert the value to.</param>
        /// <returns>A dictionary that maps each number system to its corresponding converted value.</returns>
        private static string GetFromDecimal(int decimalValue, string system)
        {
            string result = system switch
            {
                "Binary" => Binary.TranslateTo(decimalValue),
                "Hexadecimal" => Hexadecimal.TranslateTo(decimalValue),
                "Octal" => Octal.TranslateTo(decimalValue),
                "Roman" => Roman.TranslateTo(decimalValue),
                "Decimal" => decimalValue.ToString(),
                _ => throw new ArgumentException($"The numerical system '{system}' is not valid/supported."),
            };
            return result;
        }

        /// <summary>
        /// Converts a number in the given number system to its decimal representation.
        /// </summary>
        /// <param name="num">The number to convert.</param>
        /// <param name="system">The number system of the input number (e.g. Binary, Hexadecimal, Octal).</param>
        /// <returns>The decimal representation of the input number. Returns -1 if the input system is not supported.</returns>
        private static int GetToDecimal(string num, string system)
        {
            int decimalValue = system switch
            {
                "Binary" => Binary.TranslateFrom(Convert.ToInt32(num)),
                "Hexadecimal" => Hexadecimal.TranslateFrom(num),
                "Octal" => Octal.TranslateFrom(Convert.ToInt32(num)),
                "Roman" => Roman.TranslateFrom(num),
                "Decimal" => Convert.ToInt32(num),
                _ => throw new ArgumentException($"The numerical system '{system}' is not valid/supported."),
            };
            return decimalValue;
        }

        /// <summary>
        /// Translates a number from one numerical system to one or more output numerical systems.
        /// </summary>
        /// <param name="fromSystem">The input numerical system of the number.</param>
        /// <param name="number">The number to translate.</param>
        /// <param name="outputSystems">A list of output numerical systems to translate the number to.</param>
        /// <returns>A dictionary containing the translated number for each output numerical system.</returns>
        public static Dictionary<string, string> Translate(string numSystem, string number, List<string> outputSystems)
        {
            // If the input number is digit only (e.g. is not from hexadecimal or roman numerical system) and greater than 10000
            if (numSystem != "Hexadecimal" && numSystem != "Roman")
            {
                if (Convert.ToInt32(number) > MaxValueForConversion)
                {
                    throw new ArgumentOutOfRangeException(
                        $"The input number '{number}' is too large. " +
                        $"Please enter a number less than or equal to {MaxValueForConversion}.");
                }

                if (Convert.ToDouble(number) % 1 != 0)
                {
                    throw new ArgumentException($"The input number '{number}' cannot be of type double! ");
                }
            }

            // If the input system of number from user is not decimal, translate the number to decimal first.
            if (numSystem != "Decimal")
            {
                number = GetToDecimal(number, numSystem).ToString();
            }

            // Translate the number to each selected output system.
            Dictionary<string, string> translations = new();
            foreach (var outputSystem in outputSystems)
            {
                translations.Add(value: GetFromDecimal(int.Parse(number), outputSystem), key: outputSystem);
            }

            return translations;
        }
    }
}
