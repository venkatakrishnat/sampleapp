using System;

public class HardcodedSecretsExample
{
    public void ConnectToDatabase()
    {
        // Hardcoded database password (dangerous practice)
        string dbPassword = "myHardcodedPassword123!";
        string connectionString = $"Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password={dbPassword};";
        
        Console.WriteLine("Connecting to database with password: " + dbPassword);
        // Proceed to connect to the database with the hardcoded password
    }
}
