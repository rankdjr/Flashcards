using Flashcards.Database;
using Flashcards.Models;
using Flashcards.DTO;
using Flashcards.Services;
using Dapper;

namespace Flashcards.DAO;

public class StudySessionDao
{
    private readonly DatabaseContext _dbContext;

    public StudySessionDao(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void InsertNewStudySession(StudySessionDto studySession)
    {
        try
        {
            using (var dbConnection = _dbContext.GetConnectionToFlashCards())
            {
                string sql = $"INSERT INTO {ConfigSettings.tbStudySessionsName} (StackID, SessionDate, Score) VALUES (@StackID, @SessionDate, @Score)";
                dbConnection.Execute(sql, new { studySession.StackID, studySession.SessionDate, studySession.Score });
            }
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<StudySession> GetAllStudySessionsByStackId(int stackID)
    {
        try
        {
            using (var dbConnection = _dbContext.GetConnectionToFlashCards())
            {
                string sql = $"SELECT SessionID, StackID, SessionDate, Score FROM {ConfigSettings.tbStudySessionsName} WHERE StackID = @StackID";
                return dbConnection.Query<StudySession>(sql, new { stackID });
            }
        }
        catch
        {
            throw;
        }
    }

    public bool DeleteStudySession(StudySession studySession)
    {
        try
        {
            using (var dbConnection = _dbContext.GetConnectionToFlashCards())
            {
                string sql = $"DELETE FROM {ConfigSettings.tbStudySessionsName} WHERE SessionID = @SessionID";
                dbConnection.Execute(sql, new { studySession.SessionID });
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}
