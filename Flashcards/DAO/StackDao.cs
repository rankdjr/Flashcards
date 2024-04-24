using Flashcards.Database;
using Flashcards.DTO;
using Dapper;
using Flashcards.Services;

namespace Flashcards.DAO;

public class StackDao
{
    private readonly DatabaseContext _dbContext;

    public StackDao(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<StackDto> GetAllStacks()
    {
        try
        {
            using (var dbConnection = _dbContext.GetConnectionToFlashCards())
            {
                string sql = $"SELECT StackID, StackName FROM {ConfigSettings.tbStackName}";
                return dbConnection.Query<StackDto>(sql);
            }
        }
        catch
        {
            throw;
        }

    }
}
