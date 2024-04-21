using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Database;

internal class DatabaseContext
{  
    private readonly string _connectionString;
    public DatabaseContext() 
    { 
        _connectionString = "Server=localhost;Database=FLASHCARDS;User Id=myUsername;Password=myPassword;";
    }
    public void Run()
    {
        Console.WriteLine("Hello, World!");
    }
}
