using Spectre.Console;

namespace Flashcards.Services;

internal class Utilities
{
    public static void PrintNewLines(int numOfNewLines)
    {
        for (int i = 0; i < numOfNewLines; i++) { Console.WriteLine(); }
    }

    public static void DisplayExceptionErrorMessage(string message, string exception)
    {
        PrintNewLines(1);
        string errorMessage = $"[red]{message}[/]\n{exception}";
        AnsiConsole.MarkupLine(errorMessage);
        PrintNewLines(1);
    }
}
