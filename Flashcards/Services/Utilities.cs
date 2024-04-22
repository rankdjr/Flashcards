using Spectre.Console;
using Spectre.Console.Rendering;

namespace Flashcards.Services;

internal class Utilities
{
    public static string ReadFullFile(string path)
    {
        try
        {
            string fileContent = File.ReadAllText(path);
            return fileContent;
        }
        catch (Exception e)
        {
            DisplayExceptionErrorMessage("Error reading file.", e.Message);
            throw;
        }
    }

    public static void PrintNewLines(int numOfNewLines)
    {
        for (int i = 0; i < numOfNewLines; i++) { Console.WriteLine(); }
    }

    public static void DisplayExceptionErrorMessage(string message, string exception)
    {
        PrintNewLines(1);
        string errorMessage = $"[red]{message}[/]\n[bold]{exception}[/]";
        AnsiConsole.MarkupLine(errorMessage);
        PrintNewLines(1);
    }

    public static void DisplayInformationConsoleMessage(string message)
    {
        PrintNewLines(1);
        string errorMessage = $"[dim][olive]{message}[/][/]\n";
        AnsiConsole.MarkupLine(errorMessage);
        PrintNewLines(1);
    }
}
