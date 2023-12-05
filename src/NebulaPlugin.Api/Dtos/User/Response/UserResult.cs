namespace NebulaPlugin.Api.Dtos.User.Response;

public class UserResult
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public List<UserDatabaseResponse>? Dbs { get; set; }
}
