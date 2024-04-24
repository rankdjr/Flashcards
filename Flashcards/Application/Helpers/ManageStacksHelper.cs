﻿using Flashcards.DTO;
using Flashcards.DAO;
using Flashcards.Services;
using Flashcards.Models;
using Spectre.Console;
using Flashcards.Enums;

namespace Flashcards.Application.Helpers;

public class ManageStacksHelper
{
    private readonly StackDao _stackDao;
    private readonly FlashCardDao _flashCardDao;
    private readonly InputHandler _inputHandler;

    public ManageStacksHelper(StackDao stackDao, FlashCardDao flashCardDao, InputHandler inputHandler)
    {
        _stackDao = stackDao;
        _flashCardDao = flashCardDao;
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
            HandleNoStacksFound();
            return;
        }

        foreach (var stack in stacks)
        {
            AnsiConsole.MarkupLine($"[bold]{stack.StackName}[/]");
        }

        _inputHandler.PauseForContinueInput();
    }

    public void HandleNoStacksFound()
    {
        Utilities.DisplayInformationConsoleMessage("[bold]No stacks found.[/]");
        _inputHandler.PauseForContinueInput();
    }

    public void HandleStackUpdatedSuccessfully()
    {
        Utilities.DisplaySuccessMessage("Stack updated successfully.");
        _inputHandler.PauseForContinueInput();
    }

    public void HandleEditStack(StackDto stack)
    {
        AnsiConsole.Clear();
        EditStackMenuOption selectedOption = _inputHandler.PromptMenuSelection<EditStackMenuOption>();
        ExecuteSelectedEditOption(selectedOption, stack);
    }

    private void handleEndEditSelectedOptionAction(StackDto stack)
    {
        Utilities.DisplayInformationConsoleMessage("Returning to Stack Edit screen.");
        _inputHandler.PauseForContinueInput();
        HandleEditStack(stack);
    }

    private void ExecuteSelectedEditOption(EditStackMenuOption option, StackDto stack)
    {
        switch (option)
        {
            case EditStackMenuOption.EditStackName:
                EditStackName(stack);
                break;
            case EditStackMenuOption.AddFlashCard:
                EditStackAddFlashCard(stack);
                break;
            case EditStackMenuOption.DeleteFlashCard:
                EditStackDeleteFlashCard(stack);
                break;
            case EditStackMenuOption.EditFlashCard:
                // TODO: Implement edit flash card
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
        finally
        {
            handleEndEditSelectedOptionAction(stack);
        }
    }

    private void EditStackAddFlashCard(StackDto stack)
    {
        string flashCardFront = _inputHandler.PromptForNonEmptyString("Enter the front of the flash card:    ");
        string flashCardBack = _inputHandler.PromptForNonEmptyString("Enter the back of the flash card:    ");
        FlashCardDto flashCardDto = new FlashCardDto { Front = flashCardFront, Back = flashCardBack };

        try
        {
            _flashCardDao.InsertNewFlashCard(flashCardDto, stack);
            Utilities.DisplaySuccessMessage("Flash card added successfully.");
        }
        catch (Exception ex)
        {
            Utilities.DisplayExceptionErrorMessage("Error adding flash card.", ex.Message);
        }
        finally
        {
            handleEndEditSelectedOptionAction(stack);
        }
    }

    private void EditStackDeleteFlashCard(StackDto stack)
    {
        
        IEnumerable<FlashCardDto>? flashCards = GetFlashCardsByStack(stack);

        if (flashCards == null)
        {
            Utilities.DisplayInformationConsoleMessage("No flash cards found.");
            _inputHandler.PauseForContinueInput();
            return;
        }

        FlashCardDto flashCard = _inputHandler.PromptListSelectionFlashCard(flashCards, "Select a flash card to delete:");

        if (flashCard.CardID == 0)
        {
            Utilities.DisplayInformationConsoleMessage("No flash card selected.");
            _inputHandler.PauseForContinueInput();
            return;
        }

        try
        {
            bool result = _flashCardDao.DeleteFlashCard(flashCard);
            if (result)
            {
                Utilities.DisplaySuccessMessage("Flash card deleted successfully.");
            }
            else
            {
                Utilities.DisplayWarningMessage("Flash card not deleted.");
            }
        }
        catch (Exception ex)
        {
            Utilities.DisplayExceptionErrorMessage("Error deleting flash card.", ex.Message);
        }
        finally
        {
            handleEndEditSelectedOptionAction(stack);
        }
    }

    private IEnumerable<FlashCardDto>? GetFlashCardsByStack(StackDto stack)
    {
        try
        {
            var flashCards = _flashCardDao.GetAllFlashCardsByStackId(stack);
            return flashCards;
        }
        catch (Exception ex)
        {
            Utilities.DisplayExceptionErrorMessage("Error retrieving flash cards.", ex.Message);
            return null;
        }
    }
}
