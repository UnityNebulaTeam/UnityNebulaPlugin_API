using System.ComponentModel.DataAnnotations;

namespace NebulaPlugin.Api.Dtos.User;

public record AuthenticateUserDto
{
    public string? Email { get; init; }
    public string? UserName { get; init; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; init; } = null!;
}
