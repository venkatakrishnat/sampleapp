using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InsecureDeserializationExample
{
    public void DeserializeData(byte[] data)
    {
        // Dangerous: deserializing data without validation (potential for remote code execution)
        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream(data))
        {
            var obj = formatter.Deserialize(stream); // Insecure deserialization
            Console.WriteLine("Deserialized object: " + obj.ToString());
        }
    }
}
