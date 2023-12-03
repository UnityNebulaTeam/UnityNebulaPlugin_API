namespace NebulaPlugin.Api.Dtos.User;

public record AddUserDatabaseDto
{
    public string Name { get; init; }
    public string ConnectionString { get; init; }

    public AddUserDatabaseDto(string connectionString, string name)
    {
        ConnectionString = connectionString;
        Name = name;
    }
}
