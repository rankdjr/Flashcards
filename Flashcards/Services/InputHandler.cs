using Spectre.Console;

namespace Flashcards.Services;

public class InputHandler
{
    public void PauseForContinueInput()
    {
        AnsiConsole.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public bool ConfirmAction(string actionPromptMessage)
    {
        if (!AnsiConsole.Confirm(actionPromptMessage))
        {
            Utilities.DisplayCancellationMessage("Operation cancelled.");
            PauseForContinueInput();
            return false;
        }

        return true;
    }

    public TEnum PromptMenuSelection<TEnum>() where TEnum : struct, Enum
    {
        string selectedOption = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(ConfigSettings.menuTitle)
            .PageSize(ConfigSettings.pageSize)
            .AddChoices(
                Enum.GetNames(typeof(TEnum))
                    .Select(Utilities.SplitCamelCase)));

        return Enum.Parse<TEnum>(selectedOption.Replace(" ", ""));
    }
}
