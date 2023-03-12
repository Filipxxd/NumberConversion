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
            {
                int commaIndex = number.IndexOf('.');
                if (commaIndex != -1)
                {
                    number = number.Remove(commaIndex, 1).Insert(commaIndex, ",");
                }
            }

            int num;
            if (numSystem != "Decimal")
            {
                num = GetToDecimal(number, numSystem);
            }
            else
            {
                num = (int)Math.Floor(double.Parse(number));
            }

            Dictionary<string, string> translations = new();
            foreach (var outputSystem in outputSystems)
            {
                string outputValue = GetFromDecimal(num, outputSystem);
                translations.Add(outputSystem, outputValue);
            }
            return translations;
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

            string result = string.Empty;
            try
            {
                result = system switch
                {
                    "Binary" => Binary.TranslateTo(decimalValue),
                    "Hexadecimal" => Hexadecimal.TranslateTo(decimalValue),
                    "Octal" => Octal.TranslateTo(decimalValue),
                    "Roman" => Roman.TranslateTo(decimalValue),
                    _ => throw new ArgumentException($"The numerical system '{system}' is not valid/supported."),
                };
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
            int decimalValue = 0;
            try
            {
                decimalValue = system switch
                {
                    "Binary" => Binary.TranslateFrom(num),
                    "Hexadecimal" => Hexadecimal.TranslateFrom(num),
                    "Octal" => Octal.TranslateFrom(num),
                    "Roman" => Roman.TranslateFrom(num),
                    _ => throw new ArgumentException($"The numerical system '{system}' is not valid/supported."),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return decimalValue;
        }
    }
}
