using Flashcards.Services;
using System.Configuration;
using System.Data.SqlClient;

namespace Flashcards.Database;

public class DatabaseContext
{
    private readonly string _masterConnectionString;
    private readonly string _flashcardsConnectionString;

    public DatabaseContext()
    {
        _masterConnectionString =
            ConfigurationManager.
            ConnectionStrings["dbMasterConnectionString"].ConnectionString;
        _flashcardsConnectionString =
            ConfigurationManager.
            ConnectionStrings["dbFlashcardsConnectionString"].ConnectionString;
    }
    public SqlConnection GetConnectionToMaster()
    {
        var connection = new SqlConnection(_masterConnectionString);
        try
        {
            connection.Open();
            return connection;
            
        }
        catch (SqlException e)
        {
            Utilities.DisplayExceptionErrorMessage("Error opening database connection.", e.Message);
            connection.Dispose();
            throw;
        }
    }

    public SqlConnection GetConnectionToFlashCards()
    {
        var connection = new SqlConnection(_flashcardsConnectionString);
        try
        {
            connection.Open();
            return connection;
        }
        catch (SqlException e)
        {
            Utilities.DisplayExceptionErrorMessage("Error opening database connection.", e.Message);
            connection.Dispose();
            throw;
        }
    }
}
