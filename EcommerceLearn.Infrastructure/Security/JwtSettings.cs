namespace EcommerceLearn.Infrastructure.Security;

public sealed class JwtSettings
{
    public string Issuer { get; init; } = "";
    public string Audience { get; init; } = "";
    public string SigningKey { get; init; } = "";
    public int AccessTokenMinutes { get; init; } = 1200;
    public int RefreshTokenDays { get; init; } = 30;   
}