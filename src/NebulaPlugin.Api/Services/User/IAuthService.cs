using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using NebulaPlugin.Api.Dtos.User;

namespace NebulaPlugin.Api.Services.User;

public interface IAuthService
{
    Task<IdentityResult> CreateUserAsync(CreateUserDto user);
    Task<TokenDto> ValidateUserAsync(AuthenticateUserDto authUserDto);
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
    ClaimsPrincipal VerifyToken(string token);

}
