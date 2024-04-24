using Flashcards.DAO;
using Flashcards.Enums;
using Flashcards.Services;
using Flashcards.Database;
using Spectre.Console;
using Flashcards.DTO;

namespace Flashcards.Application;

public class AppManageStacks
{
    private readonly StackDao _stackDao;
    private readonly InputHandler _inputHandler;
    private readonly string _pageHeader = "Manage Stacks";
    private bool _running;


    public AppManageStacks(DatabaseContext databaseContext, InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _stackDao = new StackDao(databaseContext);
        _running = true;
    }

    public void Run()
    {
        while (_running)
        {
            AnsiConsole.Clear();
            Utilities.DisplayPageHeader(_pageHeader);
            PromptForSessionAction();
        }
    }

    private void PromptForSessionAction()
    {
        StackMenuOption selectedOption = _inputHandler.PromptMenuSelection<StackMenuOption>();
        ExecuteSelectedOption(selectedOption);
    }

    private void ExecuteSelectedOption(StackMenuOption option)
    {
        switch (option)
        {
            case StackMenuOption.ViewStacks:
                HandleViewStackSelection();
                break;
            case StackMenuOption.EditStack:
                //AppNewLogManager _appNewLogManager = new AppNewLogManager(_codingSessionDAO, _inputHandler);
                //_appNewLogManager.Run();
                break;
            case StackMenuOption.DeleteStack:
                //AppSessionManager _appSessionManager = new AppSessionManager(_codingSessionDAO, _inputHandler);
                //_appSessionManager.Run();
                break;
            case StackMenuOption.CreateStack:
                //AppGoalManager _appGoalManager = new AppGoalManager(_codingGoalDAO, _inputHandler);
                //_appGoalManager.Run();
                break;
            case StackMenuOption.Return:
                CloseSession();
                break;
        }
    }

    private void CloseSession()
    {
        _running = false;
    }

    private void HandleViewStackSelection()
    {
        AnsiConsole.Clear();
        IEnumerable<StackDto>? stacks = GetAllStacks();
        DisplayStacks(stacks);
    }

    private IEnumerable<StackDto>? GetAllStacks()
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
    private void DisplayStacks(IEnumerable<StackDto>? stacks)
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
}
