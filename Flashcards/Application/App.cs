using Flashcards.Database;

namespace Flashcards.Application;

internal class App
{
    private DatabaseContext _databaseContext;
    private bool _running;
    public App() 
    {
        _running = true;

        _databaseContext = new DatabaseContext();
    }
    public void Run()
    {
        _databaseContext.GetConnection();
        Console.WriteLine("Hello, World!");
    }

}
