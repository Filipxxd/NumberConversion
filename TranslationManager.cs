using NumberConversion.Translators;

namespace NumberConversion
{
    static class TranslationManager
    {
        public static List<string> NumericalSystems = new() { "Binary", "Roman", "Decimal", "Hexadecimal", "Octal" };

        /// <summary>
        /// Translates a number from one numerical system to one or more output numerical systems.
        /// </summary>
        /// <param name="fromSystem">The input numerical system of the number.</param>
        /// <param name="number">The number to translate.</param>
        /// <param name="outputSystems">A list of output numerical systems to translate the number to.</param>
        /// <returns>A dictionary containing the translated number for each output numerical system.</returns>
        public static Dictionary<string, string> Translate(string number, string numSystem, List<string> outputSystems)
        {
            int num = 0;
            if (IsValid(number))
            {
                if (numSystem != "Decimal")
                {
                    num = GetToDecimal(number, numSystem);
                }
                else
                {
                    num = (int)Math.Floor(double.Parse(number));
                }
            }

            Dictionary<string, string> translations = new();
            foreach (var outputSystem in outputSystems)
            {
                translations.Add(outputSystem, GetFromDecimal(num, outputSystem));
            }

            return translations;
        }

        /// <summary>
        /// Validates whether a given string is a valid number in any numerical system via custom ruleset.
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <returns>True if the string is a valid number, otherwise false.</returns>
        private static bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            int commaIndex = value.IndexOf('.');
            if (commaIndex != -1)
            {
                value = value.Remove(commaIndex, 1).Insert(commaIndex, ",");
            }

            return true;
        }

        /// <summary>
        /// Converts a decimal value to a list of pairs: number system and value.
        /// </summary>
        /// <param name="decimalValue">The decimal value to convert.</param>
        /// <param name="system">The list of number systems to convert the value to.</param>
        /// <returns>A dictionary that maps each number system to its corresponding converted value.</returns>
        private static string GetFromDecimal(int decimalValue, string system)
        {
            if (system == "Decimal")
            {
                return decimalValue.ToString();
            }

            // Math.Abs used cause Roman and Binary have no implemantation of negative TODO!
            string result = system switch
            {
                "Binary" => Binary.TranslateTo(Math.Abs(decimalValue)),
                "Hexadecimal" => Hexadecimal.TranslateTo(decimalValue),
                "Octal" => Octal.TranslateTo(decimalValue),
                "Roman" => Roman.TranslateTo(Math.Abs(decimalValue)),
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
                "Binary" => Binary.TranslateFrom(num),
                "Hexadecimal" => Hexadecimal.TranslateFrom(num),
                "Octal" => Octal.TranslateFrom(num),
                "Roman" => Roman.TranslateFrom(num),
                _ => throw new ArgumentException($"The numerical system '{system}' is not valid/supported."),
            };
            return decimalValue;
        }
    }
}
