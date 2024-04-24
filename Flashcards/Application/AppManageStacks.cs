using Flashcards.DAO;
using Flashcards.Enums;
using Flashcards.Services;
using Flashcards.Database;
using Spectre.Console;
using Flashcards.DTO;
using Flashcards.Application.Helpers;

namespace Flashcards.Application;

public class AppManageStacks
{
    private readonly StackDao _stackDao;
    private readonly InputHandler _inputHandler;
    private readonly ManageStacksHelper _manageStacksHelper;
    private readonly string _pageHeader = "Manage Stacks";
    private bool _running;


    public AppManageStacks(DatabaseContext databaseContext, InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _stackDao = new StackDao(databaseContext);
        _manageStacksHelper = new ManageStacksHelper(_stackDao, _inputHandler);

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
                HandleEditStackSelection();
                break;
            case StackMenuOption.DeleteStack:
                // TODO: Implement delete stack
                //AppSessionManager _appSessionManager = new AppSessionManager(_codingSessionDAO, _inputHandler);
                //_appSessionManager.Run();
                break;
            case StackMenuOption.CreateStack:
                // TODO: Implement create stack
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
        IEnumerable<StackDto>? stacks = _manageStacksHelper.GetAllStacks();
        _manageStacksHelper.DisplayStacks(stacks);
    }

    private void HandleEditStackSelection()
    {
        AnsiConsole.Clear();
        IEnumerable<StackDto>? stacks = _manageStacksHelper.GetAllStacks();

        // TODO: This code is repeated, consider refactoring See ManageStacksHelper.cs Line 35
        if (stacks == null)
        {
            Utilities.DisplayInformationConsoleMessage("No stacks found.");
            _inputHandler.PauseForContinueInput();
            return;
        }

        StackDto stack = _inputHandler.PromptForSelectionListStacks(stacks, "Select a stack to edit:");

        _manageStacksHelper.HandleEditStack(stack);
    }
}
