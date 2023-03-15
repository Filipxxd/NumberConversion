using Spectre.Console;
using System.Text.RegularExpressions;
using static NumberConversion.TranslationManager;

namespace NumberConversion
{
    partial class Program
    {
        public static void Main(string[] args)
        {
            if (!AnsiConsole.Profile.Capabilities.Interactive)
            {
                Console.WriteLine("Environment does not support interaction");
                return;
            }

            if (Console.BackgroundColor != ConsoleColor.Black)
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }

            AnsiConsole.Write(new FigletText("Number Conversion").Centered().Color(Color.Red));

            bool askAgain = true;
            while (askAgain)
            {
                WriteDivider("Number");
                string number = AskInputNumber();

                WriteDivider("Input Numerical System");
                string inputSystem = AskInputSystem();

                long decimalNumber;
                try
                {
                    NumericalSystem system = IdentifySystem(inputSystem);
                    decimalNumber = TryConversionFrom(system, number);

                    WriteDivider("Output Numerical System(s)");
                    List<string> outputSystems = AskOutputSystems(inputSystem);

                    WriteDivider("Result");
                    WriteTranslations(outputSystems, decimalNumber);

                    askAgain = false;
                }
                catch (Exception)
                {
                    AnsiConsole.MarkupLine($"[red]There was an error while trying to convert number into '{inputSystem}'.[/]");
                    askAgain = AnsiConsole.Confirm("Do you want to start over?");
                }
            }

            WriteDivider("Press Any To Close");
            Console.ReadLine();
        }

        /// <summary>
        /// Writes a divider to the console with the specified text.
        /// </summary>
        /// <param name="text">The text to display in the divider.</param>
        private static void WriteDivider(string text)
        {
            AnsiConsole.Write(new Rule(Environment.NewLine + $"[yellow]{text}[/]").RuleStyle("grey").LeftJustified());
        }

        [GeneratedRegex("^[a-zA-Z0-9]+$")]
        private static partial Regex InputRegex();
        private static readonly int MaxCharactersCount = 50;

        /// <summary>
        /// Asks the user to input a number to translate.
        /// </summary>
        /// <returns>The user's input.</returns>
        private static string AskInputNumber()
        {
            Regex regex = InputRegex();

            return AnsiConsole.Prompt(new TextPrompt<string>("Please type in a [green]number[/] to translate: ")
                .PromptStyle("green")
                .Validate(userInput =>
                    regex.IsMatch(userInput) && userInput.Length < MaxCharactersCount ?
                        ValidationResult.Success() :
                        ValidationResult.Error(
                            "[red]Please type in a valid number that has no more than 50 chars.[/]" +
                            Environment.NewLine +
                            $"[red]Bear in mind that only positive whole numbers are accepted![/]")));
        }

        /// <summary>
        /// Asks the user to select the input numerical system.
        /// </summary>
        /// <returns>The user's selection.</returns>
        private static string AskInputSystem()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which [green]numerical system[/] is your number from?")
                    .MoreChoicesText("[grey](Move up and down via arrow keys)[/]")
                    .AddChoices(Enum.GetNames(typeof(NumericalSystem))));
        }

        /// <summary>
        /// Asks the user to select the output numerical system(s).
        /// </summary>
        /// <returns>The user's selection.</returns>
        private static List<string> AskOutputSystems(string selectedSystem)
        {
            List<string> availableSystems = new(Enum.GetNames(typeof(NumericalSystem)));
            availableSystems.Remove(selectedSystem);

            var selection = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("Which [green]numerical system(s)[/] do you want to translate into?")
                    .MoreChoicesText("[grey](Move up and down via arrow keys)[/]")
                    .InstructionsText("[grey](Press [blue]<spacebar>[/] to select a numerical system, [green]<enter>[/] to accept)[/]")
                    .AddChoices(availableSystems));

            selection.Reverse();
            return selection;
        }

        /// <summary>
        /// Displays the results of a translation process to the console.
        /// </summary>
        /// <param name="translations">A dictionary containing the translations.</param>
        private static void WriteTranslations(List<string> outputSystems, long decimalNumber)
        {
            var table = new Table().AddColumns("[grey]Numerical system[/]", "[grey]Translation[/]").Centered();
            table.Columns[1].Alignment(Justify.Right);

            foreach (string outputSystem in outputSystems)
            {
                NumericalSystem outputNumericalSystem = IdentifySystem(outputSystem);
                table.AddRow(outputSystem, Translate(decimalNumber, outputNumericalSystem));
            }
            AnsiConsole.Write(table);
        }
    }
}
