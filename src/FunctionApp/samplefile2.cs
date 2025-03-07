using System;
using System.Security.Cryptography;
using System.Text;

public class InsecureCryptographyExample
{
    public void EncryptData(string data)
    {
        // Using MD5 (weak hashing algorithm)
        using (MD5 md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
            Console.WriteLine("MD5 hash: " + BitConverter.ToString(hash).Replace("-", ""));
        }
    }
}
