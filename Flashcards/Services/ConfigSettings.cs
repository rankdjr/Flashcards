namespace Flashcards.Services;

public class ConfigSettings
{
    public static string dbMasterConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbMasterConnectionString"].ConnectionString;
    public static string dbFlashcardsConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbFlashcardsConnectionString"].ConnectionString;
    public static string createScriptPath = System.Configuration.ConfigurationManager.AppSettings["CreateScriptFpath"]!;
    public static string validateScriptPath = System.Configuration.ConfigurationManager.AppSettings["ValidateScriptFpath"]!;
    public static string dbCreateFileName = System.Configuration.ConfigurationManager.AppSettings["DbCreateFileName"]!;
    public static string tbStackCreateFileName = System.Configuration.ConfigurationManager.AppSettings["TblStackCreateFileName"]!;
    public static string tbFlashCardsCreateFileName = System.Configuration.ConfigurationManager.AppSettings["TblFlashCardsCreateFileName"]!;
    public static string tbStudySessionsCreateFileName = System.Configuration.ConfigurationManager.AppSettings["TblStudySessionsCreateFileName"]!;
    public static string vwFlashCardsCreateFileName = System.Configuration.ConfigurationManager.AppSettings["VwFlashCardsCreateFileName"]!;
    public static string vwStudySessionsCreateFileName = System.Configuration.ConfigurationManager.AppSettings["VwStudySessionsCreateFileName"]!;
    public static string vwFlashCardsRenumberedCreateFileName = System.Configuration.ConfigurationManager.AppSettings["VwFlashCardsRenumberedCreateFileName"]!;
    public static string dbName = System.Configuration.ConfigurationManager.AppSettings["DbName"]!;
    public static string tbStackName = System.Configuration.ConfigurationManager.AppSettings["TblStackName"]!;
    public static string tbFlashCardsName = System.Configuration.ConfigurationManager.AppSettings["TblFlashCardsName"]!;
    public static string tbStudySessionsName = System.Configuration.ConfigurationManager.AppSettings["TblStudySessionsName"]!;
    public static string vwFlashCardsName = System.Configuration.ConfigurationManager.AppSettings["VwFlashCardsName"]!;
    public static string vwStudySessionsName = System.Configuration.ConfigurationManager.AppSettings["VwStudySessionsName"]!;
    public static string vwFlashCardsRenumberedName = System.Configuration.ConfigurationManager.AppSettings["VwFlashCardsRenumberedName"]!;
}
