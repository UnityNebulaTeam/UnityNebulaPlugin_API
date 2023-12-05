namespace NebulaPlugin.Api.Dtos.User;

public record TokenDto
{
    public string Token { get; init; }
    public string RefreshToken { get; init; }

    public TokenDto(string token, string refreshToken)
    {
        Token = token;
        RefreshToken = refreshToken;
    }

}
