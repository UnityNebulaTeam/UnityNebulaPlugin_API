using System.Data;
using NebulaPlugin.Common.Enums;

namespace NebulaPlugin.Api.Dtos.User;

public record AddUserDatabaseDto
{
    public string Name { get; init; }
    public string ConnectionString { get; init; }
    public string KeyIdentifier { get; init; }

    public AddUserDatabaseDto(string connectionString, string name, string keyIdentifier)
    {
        ConnectionString = connectionString;
        Name = name;

        foreach (var key in Enum.GetValues(typeof(DbTypes)))
        {
            Console.WriteLine($"----------------------------------------------------------______>{key} - {keyIdentifier}");
            if (string.Equals(key.ToString(), keyIdentifier))
                KeyIdentifier = keyIdentifier;


        }

        // if (KeyIdentifier is null)
        //     throw new ArgumentException("Invalid DbType provided: " + keyIdentifier);




    }
}
