using System.Data.SqlClient;
using Flashcards.Services;
using Dapper;
using System.Configuration;

namespace Flashcards.Database;

public class DatabaseInitializer
{
    private readonly DatabaseContext _dbContext;
    private readonly string _sqlCreateScriptFilePath;

    public DatabaseInitializer(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
        _sqlCreateScriptFilePath = ConfigurationManager.AppSettings["CreateScriptFpath"]!;
    }

    public void InitializeDatabase()
    {
        try
        {
            CreateDatabase();
            CreateTables();
            CreateViews();
        }
        catch (SqlException e)
        {
            Utilities.DisplayExceptionErrorMessage("Error creating database.", e.Message);
            Utilities.DisplayInformationConsoleMessage("Exiting application.");
            Environment.Exit(1);
        }
    }

    private void CreateDatabase()
    {
        try
        {
            string sqlScriptFilePath = Path.Combine(_sqlCreateScriptFilePath, ConfigSettings.dbCreateFileName);
            string sql = Utilities.ReadFullFile(sqlScriptFilePath);

            using (SqlConnection connection = _dbContext.GetConnectionToMaster())
            {
                connection.Execute(sql);

            }
        }
        catch (SqlException e)
        {
            Utilities.DisplayExceptionErrorMessage("Error creating database.", e.Message);
            throw;
        }
    }

    private void CreateTables()
    {
        string[] createTableFiles = new string[] {
            ConfigSettings.tbStackCreateFileName,
            ConfigSettings.tbFlashCardsCreateFileName,
            ConfigSettings.tbStudySessionsCreateFileName
        };

        try
        {
            foreach (string fileContent in createTableFiles)
            {
                string sqlScriptFilePath = Path.Combine(_sqlCreateScriptFilePath, fileContent);
                string sql = Utilities.ReadFullFile(sqlScriptFilePath);

                using (SqlConnection connection = _dbContext.GetConnectionToFlashCards())
                {
                    connection.Execute(sql);
                }
            }
        }
        catch (SqlException e)
        {
            Utilities.DisplayExceptionErrorMessage("Error creating tables.", e.Message);
            throw;
        }
    }

    private void CreateViews()
    {
        string[] createViewFiles = new string[] {
            ConfigSettings.vwFlashCardsCreateFileName,
            ConfigSettings.vwFlashCardsRenumberedCreateFileName,
            ConfigSettings.vwStudySessionsCreateFileName
        };

        try
        {
            foreach (string fileContent in createViewFiles)
            {
                string sqlScriptFilePath = Path.Combine(_sqlCreateScriptFilePath, fileContent);
                string sql = Utilities.ReadFullFile(sqlScriptFilePath);

                using (SqlConnection connection = _dbContext.GetConnectionToFlashCards())
                {
                    connection.Execute(sql);
                }
            }
        }
        catch (SqlException e)
        {
            Utilities.DisplayExceptionErrorMessage("Error creating views.", e.Message);
            throw;
        }
    }

    private void CheckDatabaseExists()
    {
        try
        {
            using (SqlConnection connection = _dbContext.GetConnectionToMaster())
            {
                string sql = "SELECT COUNT(*) FROM sys.databases WHERE name = 'Flashcards'";
                int count = connection.QuerySingle<int>(sql);
                if (count == 0)
                {
                    throw new Exception("Database does not exist.");
                }
            }
        }
        catch (SqlException e)
        {
            Utilities.DisplayExceptionErrorMessage("Error checking if database exists.", e.Message);
            throw;
        }
    }

    private void CheckTablesExist()
    {
        try
        {
            using (SqlConnection connection = _dbContext.GetConnectionToFlashCards())
            {
                string sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Flashcards'";
                int count = connection.QuerySingle<int>(sql);
                if (count == 0)
                {
                    throw new Exception("Tables do not exist.");
                }
            }
        }
        catch (SqlException e)
        {
            Utilities.DisplayExceptionErrorMessage("Error checking if tables exist.", e.Message);
            throw;
        }
    }

    private void CheckViewsExist()
    {
        try
        {
            using (SqlConnection connection = _dbContext.GetConnectionToFlashCards())
            {
                string sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'Flashcards'";
                int count = connection.QuerySingle<int>(sql);
                if (count == 0)
                {
                    throw new Exception("Views do not exist.");
                }
            }
        }
        catch (SqlException e)
        {
            Utilities.DisplayExceptionErrorMessage("Error checking if views exist.", e.Message);
            throw;
        }
    }
}
