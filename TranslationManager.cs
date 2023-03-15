using NumberConversion.Translators;

namespace NumberConversion
{
    /// <summary>
    /// Provides methods to translate numbers between different numerical systems.
    /// </summary>
    static class TranslationManager
    {
        /// <summary>
        /// Availible Numerical systems for conversion to or from one another.
        /// </summary>
        public enum NumericalSystem
        {
            Binary,
            Roman,
            Decimal,
            Hexadecimal,
            Octal
        }

        /// <summary>
        /// Translates a number to the specified numerical system.
        /// </summary>
        /// <param name="system">The target numerical system.</param>
        /// <param name="number">The number to translate.</param>
        /// <returns>The translated number as a string.</returns>
        public static string Translate(long number, NumericalSystem system)
        {
            string translation;
            try
            {
                translation = system switch
                {
                    NumericalSystem.Binary => Binary.TranslateTo(number),
                    NumericalSystem.Hexadecimal => Hexadecimal.TranslateTo(number),
                    NumericalSystem.Octal => Octal.TranslateTo(number),
                    NumericalSystem.Roman => Roman.TranslateTo(number),
                    NumericalSystem.Decimal => number.ToString(),
                    _ => throw new ArgumentException($"The numerical system '{system}' is not valid/supported."),
                };
            }
            catch (ArgumentNullException)
            {
                translation = "null";
            }
            catch (ArgumentOutOfRangeException)
            {
                translation = "out-of-range";
            }
            catch (ArgumentException)
            {
                translation = "argument-except";
            }
            catch (InvalidOperationException)
            {
                translation = "invalid-operation";
            }
            catch (OverflowException)
            {
                translation = "overflow";
            }
            catch (Exception)
            {
                translation = "except";
            }

            return translation;
        }

        /// <summary>
        /// Translates a number from the specified numerical system into Int64 Decimal value.
        /// </summary>
        /// <param name="system">The number´s original numerical system.</param>
        /// <param name="number">The number to translate.</param>
        /// <returns>The translated number as a string.</returns>
        public static long TryConversionFrom(NumericalSystem system, string number)
        {
            return system switch
            {
                NumericalSystem.Binary => Binary.TranslateFrom(number),
                NumericalSystem.Hexadecimal => Hexadecimal.TranslateFrom(number),
                NumericalSystem.Octal => Octal.TranslateFrom(number),
                NumericalSystem.Roman => Roman.TranslateFrom(number),
                NumericalSystem.Decimal => Convert.ToInt64(number),
                _ => throw new ArgumentException($"The numerical system '{system}' is not valid/supported."),
            };
        }

        public static NumericalSystem IdentifySystem(string possibleSystem)
        {
            if (!Enum.TryParse(possibleSystem, true, out NumericalSystem system))
            {
                throw new ArgumentException($"System '{possibleSystem}' is not defined.");
            }
            return system;
        }
    }
}
