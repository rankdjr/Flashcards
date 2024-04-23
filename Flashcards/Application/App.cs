using Flashcards.Database;
using Flashcards.Services;

namespace Flashcards.Application;

internal class App
{
    private DatabaseContext _databaseContext;
    private AppConfigurationHandler _appConfigurationHandler;
    private InputHandler _inputHandler;
    private bool _running;

    public App() 
    {
        _running = true;

        // Configure Application
        _inputHandler = new InputHandler();
        _databaseContext = new DatabaseContext();
        _appConfigurationHandler = new AppConfigurationHandler(_databaseContext, _inputHandler);
        _appConfigurationHandler.ConfigureApplication();

        // Setup Services

    }

    public void Run()
    {
        Console.WriteLine("Hello, World!");
    }
}
