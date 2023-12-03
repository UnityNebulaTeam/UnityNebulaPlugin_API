using System.ComponentModel.DataAnnotations;

namespace NebulaPlugin.Api.Dtos.User;

public record CreateUserDto
{

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; init; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; init; } = null!;
    private string? _userName;

    public string UserName
    {
        get => CreateUserNameIfUserNameNull();
        init => _userName = value;
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

    public CreateUserDto(string email, string password)
    {
        Email = email;
        Password = password;
    }

}
