using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NebulaPlugin.Api.Dtos.User;
using NebulaPlugin.Api.EFCore;


namespace NebulaPlugin.Api.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<Models.User> _userManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<Models.User> userManager, IConfiguration configuration, AppDbContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
    }

    public async Task<IdentityResult> CreateUserAsync(CreateUserDto userDto)
    {
        var dbUser = await _userManager.FindByEmailAsync(userDto.Email);

        if (dbUser is not null)
            throw new Exception("Email already taken");

        Models.User user = new()
        {
            Email = userDto.Email,
            UserName = userDto.UserName,
        };

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded)
            throw new AggregateException(result.Errors.Select(e => e.Description)
                                                      .Select(e => new Exception(e)));

        return result;
    }
    public async Task<TokenDto> ValidateUserAsync(AuthenticateUserDto authUserDto)
    {
        Models.User? dbUser = string.IsNullOrEmpty(authUserDto.Email)
                            ? await _userManager.Users.Include(u => u.Databases).FirstOrDefaultAsync(u => u.UserName == authUserDto.UserName)
                            : await _userManager.Users.Include(u => u.Databases).FirstOrDefaultAsync(u => u.Email == authUserDto.Email);

        if (dbUser is null)
            throw new Exception("user not found by this email or username");

        bool result = await _userManager.CheckPasswordAsync(dbUser, authUserDto.Password);

        if (!result)
            throw new Exception("Email/Username and Password is not correct");

        var token = await CreateToken(dbUser);

        return token;


    }
    public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
    {
        var claimsPrincipal = VerifyToken(tokenDto.Token);

        var jwtSecurityToken = claimsPrincipal;

        Claim? emailClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        var userWithDatabases = await _context.Users.Include(u => u.Databases).FirstOrDefaultAsync(u => u.Email == emailClaim.Value);

        bool correctRefreshToken = string.Equals(userWithDatabases.RefreshToken, tokenDto.RefreshToken);

        if (!correctRefreshToken)
            throw new Exception("Wrong refresh token please enter valid refresh token");

        var newToken = await CreateToken(userWithDatabases);

        return newToken;
    }


    #region Helpers

    private ClaimsPrincipal VerifyToken(string token)
    {
        JwtSecurityTokenHandler _tokenHandler = new();

        var jwtSettings = _configuration.GetSection("JwtSettings");

        var tokenValidationParams = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["issuer"],
            ValidAudience = jwtSettings["audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secret"])),
            RequireExpirationTime = true
        };

        ClaimsPrincipal? principal = _tokenHandler.ValidateToken(token: token, validationParameters: tokenValidationParams, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("invalid token.");

        return principal;

    }

    private async Task<TokenDto> CreateToken(Models.User user)
    {
        var signinCredentials = GetSinginCredentials();
        var claims = await GetUserClaims(user);
        var tokenOptions = GenerateTokenOption(claims, signinCredentials);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        string refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpireDate = DateTime.Now.AddDays(14);

        await _userManager.UpdateAsync(user);

        return new(accessToken, refreshToken);

    }

    private async Task<List<Claim>> GetUserClaims(Models.User user)
    {
        var userDatabasesJsonObj = JsonSerializer.Serialize(user.Databases.Select(db => db.KeyIdentifier));
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("dbs" , userDatabasesJsonObj),
        };

        return claims;
    }

    private SigningCredentials GetSinginCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secret"]));
        return new(key: key, SecurityAlgorithms.HmacSha256);
    }

    private JwtSecurityToken GenerateTokenOption(List<Claim> claims, SigningCredentials credentials)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        return new JwtSecurityToken(
            issuer: jwtSettings["issuer"],
            audience: jwtSettings["audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: credentials
        );
    }

    private string GenerateRefreshToken()
    {
        var random = new byte[32];

        using var range = RandomNumberGenerator.Create();

        range.GetBytes(random);

        return Convert.ToBase64String(random);
    }

    #endregion

}
