
namespace NebulaPlugin.Api.Dtos.User;

public record CreateUserDto
{
    public string Password { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string UserName { get => CreateUserNameIfUserNameNull(); init => _userName = value; }
    private string? _userName;

    public AddUserDatabaseDto Db { get; set; }
    public CreateUserDto(string email, string password, AddUserDatabaseDto db)
    {
        Email = email;
        Password = password;
        Db = db;
    }

    private string CreateUserNameIfUserNameNull()
    {
        if (string.IsNullOrEmpty(_userName) && !string.IsNullOrEmpty(Email))
        {
            string[] emailParts = Email.Split("@", StringSplitOptions.None);
            var nameWithoutChars = emailParts[0].Replace("_", "").Replace("-", "").Replace(".", "");
            _userName = nameWithoutChars + "_" + new Random().Next(0, 9999).ToString();
        }
        return _userName ?? string.Empty;
    }


}
