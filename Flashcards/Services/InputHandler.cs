using Spectre.Console;
using Flashcards.DTO;

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

    public StackDto PromptForSelectionListStacks(IEnumerable<StackDto> stacks, string promptMessage)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<StackDto>()
                .Title(promptMessage)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to see more log entries)[/]")
                .UseConverter(stack => stack.StackName!)
                .AddChoices(stacks));
    }

    public string PromptForNonEmptyString(string promptMessage)
    {
        string userInput = AnsiConsole.Prompt(
            new TextPrompt<string>(promptMessage)
                .PromptStyle("yellow")
                .Validate(input =>
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Success();
                    }
                    else
                    {
                        var errorMessage = "[red]Input cannot be empty.[/]";
                        return ValidationResult.Error(errorMessage);
                    }
                }));

        return userInput.Trim();
    }
}
