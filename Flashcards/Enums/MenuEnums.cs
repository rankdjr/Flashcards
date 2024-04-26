﻿namespace Flashcards.Enums;

public enum MainMenuOption
{
    Exit,
    StartStudySession,
    ManageStacks,
    ManageFlashCards,
    ViewStudySessionData,
    SeedDatabase
}

public enum StackMenuOption
{
    Return,
    ViewStacks,
    EditStack,
    DeleteStack,
    CreateStack,
}

public enum EditStackMenuOption
{
    Cancel,
    EditStackName,
    AddFlashCard,
    DeleteFlashCard,
    EditFlashCard
}

public enum FlashCardMenuOption
{
    Cancel,
    EditFlashCard,
    DeleteFlashCard
}