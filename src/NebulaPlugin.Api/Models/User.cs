using Microsoft.AspNetCore.Identity;

namespace NebulaPlugin.Api.Models;

public class User : IdentityUser
{

    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }

    public ICollection<Connection>? Connections { get; set; }
}
