using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Application.Interfaces.Security;

public interface IJwtTokenService
{
    string GeneratePasswordResetToken(User user);
    string GenerateAccessToken(User user);

    string GenerateRefreshToken(User user);
}