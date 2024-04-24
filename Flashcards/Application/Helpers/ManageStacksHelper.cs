using Flashcards.DTO;
using Flashcards.DAO;
using Flashcards.Services;
using Spectre.Console;
using Flashcards.Enums;

namespace Flashcards.Application.Helpers;

public class ManageStacksHelper
{
    private readonly StackDao _stackDao;
    private readonly InputHandler _inputHandler;

    public ManageStacksHelper(StackDao stackDao, InputHandler inputHandler)
    {
        _stackDao = stackDao;
        _inputHandler = inputHandler;
    }

    public IEnumerable<StackDto>? GetAllStacks()
    {
        try
        {
            return _stackDao.GetAllStacks();
        }
        catch (Exception ex)
        {
            Utilities.DisplayExceptionErrorMessage("Error retrieving stacks.", ex.Message);
            return null;
        }
    }

    public void DisplayStacks(IEnumerable<StackDto>? stacks)
    {
        if (stacks == null)
        {
            AnsiConsole.MarkupLine("[bold]No stacks found.[/]");
            _inputHandler.PauseForContinueInput();
            return;
        }

        foreach (var stack in stacks)
        {
            AnsiConsole.MarkupLine($"[bold]{stack.StackName}[/]");
        }

        _inputHandler.PauseForContinueInput();
    }

    public void HandleEditStack(StackDto stack)
    {
        AnsiConsole.Clear();
        EditStackMenuOption selectedOption = _inputHandler.PromptMenuSelection<EditStackMenuOption>();
        ExecuteSelectedEditOption(selectedOption, stack);
    }

    private void ExecuteSelectedEditOption(EditStackMenuOption option, StackDto stack)
    {
        switch (option)
        {
            case EditStackMenuOption.EditStackName:
                EditStackName(stack);
                break;
            case EditStackMenuOption.AddFlashCard:
                //AppNewLogManager _appNewLogManager = new AppNewLogManager(_codingSessionDAO, _inputHandler);
                //_appNewLogManager.Run();
                break;
            case EditStackMenuOption.DeleteFlashCard:
                //AppSessionManager _appSessionManager = new AppSessionManager(_codingSessionDAO, _inputHandler);
                //_appSessionManager.Run();
                break;
            case EditStackMenuOption.Cancel:
                return;
        }
    }

    private void EditStackName(StackDto stack)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[bold]Editing stack:[/]    [purple]{stack.StackName}[/]");
        var allStacks = _stackDao.GetAllStacks();

        do
        {
            stack.StackName = _inputHandler.PromptForNonEmptyString("Enter a new name for the stack:    ");

            if (allStacks.Any(s => Utilities.StringTrimLower(s.StackName!) == Utilities.StringTrimLower(stack.StackName)))
            {
                Utilities.DisplayWarningMessage("A stack with that name already exists.");
            }
        } while (allStacks.Any(s => Utilities.StringTrimLower(s.StackName!) == Utilities.StringTrimLower(stack.StackName)));

        try
        {
            _stackDao.UpdateStackName(stack);
            Utilities.DisplaySuccessMessage("Stack updated successfully.");
        }
        catch (Exception ex)
        {
            Utilities.DisplayExceptionErrorMessage("Error updating stack.", ex.Message);
        }

        _inputHandler.PauseForContinueInput();
    }
}
