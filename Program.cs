using Spectre.Console;

namespace NumberConversion
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Check if the console supports interactive features.
            if (!AnsiConsole.Profile.Capabilities.Interactive)
            {
                AnsiConsole.MarkupLine("[red]Environment does not support interaction.[/]");
                return;
            }

            // Displays header
            AnsiConsole.Write(
                new FigletText("Number Conversion")
                    .Centered()
                    .Color(Color.Red));

            // Ask the user for the number to translate.
            WriteDivider("Number");
            string number = AskInputNumber();

            // Ask the user for the input numerical system.
            WriteDivider("Input Numerical System");
            string inputSystem = AskInputSystem();

            // Ask the user for the output numerical systems.
            WriteDivider("Output Numerical System(s)");
            List<string> outputSystems = AskOutputSystems();

            // Display the results in a table.
            WriteDivider("Answer");
            try
            {
                DisplayTranslationsToConsole(TranslationManager.Translate(inputSystem, number, outputSystems));
            }
            catch (FormatException ex)
            {
                AnsiConsole.Write($"{ex.GetType().Name}: {number} is not valid number in {inputSystem} system!");
            }
            catch (Exception ex)
            {
                AnsiConsole.Write($"{ex.GetType().Name}: {ex.Message}");
            }

            // Confirm closing the app.
            WriteDivider("Press Any To Close");
            Console.ReadLine();

        }

        /// <summary>
        /// Displays the results of a translation process to the console.
        /// </summary>
        /// <param name="translations">A dictionary containing the translations.</param>
        private static void DisplayTranslationsToConsole(Dictionary<string, string> translations)
        {
            var table = new Table().AddColumns("[grey]Numerical system[/]", "[grey]Translation[/]").Centered();
            foreach (var item in translations)
            {
                table.AddRow(item.Key, item.Value).Centered();
            }
            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Writes a divider to the console with the specified text.
        /// </summary>
        /// <param name="text">The text to display in the divider.</param>
        private static void WriteDivider(string text)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Rule($"[yellow]{text}[/]").RuleStyle("grey").LeftJustified());
        }

        /// <summary>
        /// Asks the user to input a number to translate.
        /// </summary>
        /// <returns>The user's input.</returns>
        private static string AskInputNumber()
        {
            return AnsiConsole.Ask<string>("Please type in a [green]number[/] to translate: ");
        }

        /// <summary>
        /// Asks the user to select the input numerical system.
        /// </summary>
        /// <returns>The user's selection.</returns>
        private static string AskInputSystem()
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which [green]numerical system[/] is your number from?")
                    .MoreChoicesText("[grey](Move up and down via arrow keys)[/]")
                    .AddChoices(TranslationManager.NumericalSystems));

            // Remove the user selected input system from the list of all systems.
            TranslationManager.NumericalSystems.Remove(selection);

            return selection;
        }

        /// <summary>
        /// Asks the user to select the output numerical system(s).
        /// </summary>
        /// <returns>The user's selection.</returns>
        private static List<string> AskOutputSystems()
        {
            var selection = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .PageSize(10)
                    .Title("Which [green]numerical system(s)[/] do you want to translate into?")
                    .MoreChoicesText("[grey](Move up and down via arrow keys)[/]")
                    .InstructionsText("[grey](Press [blue]<spacebar>[/] to select a numerical system, [green]<enter>[/] to accept)[/]")
                    .AddChoices(TranslationManager.NumericalSystems));

            // To make output in same order as selection - ascending.
            selection.Reverse();
            return selection;
        }


    }
}
