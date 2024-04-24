using Flashcards.DAO;
using Flashcards.Services;

namespace Flashcards.Application;

public class AppManageFlashCards
{
    private readonly FlashCardDao _flashCardDao;
    private readonly InputHandler _inputHandler;

    public AppManageFlashCards(FlashCardDao flashCardDao, InputHandler inputHandler)
    {
        _flashCardDao = flashCardDao;
        _inputHandler = inputHandler;
    }

    public void Run()
    {
        Console.WriteLine("Flashcards");
    }
}
