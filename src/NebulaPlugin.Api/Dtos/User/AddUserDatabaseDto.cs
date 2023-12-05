using System.Data;
using NebulaPlugin.Common.Enums;

namespace NebulaPlugin.Api.Dtos.User;

public record AddUserDatabaseDto
{
    public string ConnectionString { get; init; }
    public string KeyIdentifier { get; init; }

    public AddUserDatabaseDto(string connectionString, string keyIdentifier)
    {
        ConnectionString = connectionString;

        if (!Enum.TryParse<DbTypes>(keyIdentifier, false, out var type))
            throw new ArgumentException("Invalid DbType provided: " + keyIdentifier);


        KeyIdentifier = type.ToString();




    }
}
