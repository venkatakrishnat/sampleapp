using System;
using System.IO;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        // Issue 1: Hardcoded Password (Sensitive Information Exposure)
        string password = "SuperSecretPassword";  // Hardcoded secret

        // Issue 2: SQL Injection (Unescaped User Input in SQL Query)
        string userInput = "123'; DROP TABLE Users; --"; // Malicious user input
        string query = "SELECT * FROM Users WHERE ID = '" + userInput + "'";  // SQL Injection risk

        // Issue 3: Unchecked user input for file path (Path Traversal)
        string filePath = "../../../etc/passwd";  // Potential path traversal
        try
        {
            string content = File.ReadAllText(filePath);  // File access based on user input
            Console.WriteLine(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
        }

        // Issue 4: Unvalidated external input to open a file (Unrestricted File Access)
        string externalFilePath = Console.ReadLine(); // External input for file path
        try
        {
            // Opening any file based on user input, risky if not validated
            File.ReadAllText(externalFilePath);  
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error opening file: " + ex.Message);
        }

        // Issue 5: Unsecure Connection String (Hardcoded Database Credentials)
        SqlConnection connection = new SqlConnection("Server=myserver;Database=mydb;User Id=myuser;Password=myPassword;"); // Hardcoded connection string
        try
        {
            connection.Open();
            Console.WriteLine("Database connection established.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Database connection failed: " + ex.Message);
        }
    }
}
