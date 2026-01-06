using EcommerceLearn.Domain.Entities.Users;
using EcommerceLearn.Domain.Entities.Auth;

namespace EcommerceLearn.Application.Features.Auth.Services;

public interface IAuthEmailService
{
    Task<EmailVerification> SendEmailVerification(User user);
    Task<PasswordVerification> SendPasswordVerification(User user);
}