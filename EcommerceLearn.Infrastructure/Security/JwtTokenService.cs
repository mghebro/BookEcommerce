using EcommerceLearn.Application.Interfaces.Security;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using EcommerceLearn.Domain.Entities.Users;
using System.Security.Claims;
using System.Text;

namespace EcommerceLearn.Infrastructure.Security;

public sealed class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _s;

    public JwtTokenService(JwtSettings s)
    {
        _s = s;
    }

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_s.SigningKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _s.Issuer,
            audience: _s.Audience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_s.AccessTokenMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GeneratePasswordResetToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email.Value),
            new("pwd_reset", "true"),
            new("CODE", user.PasswordEmailVerification?.Code ?? string.Empty)
        };

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_s.SigningKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _s.Issuer,
            audience: _s.Audience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public string GenerateRefreshToken(User user)
    {
        throw new NotImplementedException();
    }
}