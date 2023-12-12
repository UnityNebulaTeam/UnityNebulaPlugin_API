using NebulaPlugin.Common.Enums;

namespace NebulaPlugin.Api.Dtos.User;

public record UpdateUserDatabaseDto
{
    public string ConnectionString { get; init; }
    public UpdateUserDatabaseDto(string connectionString)
    {
        ConnectionString = connectionString;

    }
}
