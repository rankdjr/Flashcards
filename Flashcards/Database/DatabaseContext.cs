using Flashcards.Services;
using System.Configuration;
using System.Data.SqlClient;

namespace Flashcards.Database;

internal class DatabaseContext
{
    private readonly string _connectionString;
    public DatabaseContext()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;
    }
    public SqlConnection GetConnection()
    {
        var connection = new SqlConnection(_connectionString);
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
