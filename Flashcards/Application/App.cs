using Flashcards.Database;

namespace Flashcards.Application;

internal class App
{
    private DatabaseContext _databaseContext;
    private DatabaseInitializer _databaseInitializer;
    private bool _running;

    public App() 
    {
        _running = true;

        // Setup Database
        _databaseContext = new DatabaseContext();
        _databaseInitializer = new DatabaseInitializer(_databaseContext);
        _databaseInitializer.InitializeDatabase();

        // Setup Services
    }
    public void Run()
    {
        Console.WriteLine("Hello, World!");
    }
}
