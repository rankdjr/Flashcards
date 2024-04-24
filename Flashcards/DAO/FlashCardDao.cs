using Flashcards.Database;

namespace Flashcards.DAO;

public class FlashCardDao
{
    private readonly DatabaseContext _dbContext;

    public FlashCardDao(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
}
