using Microsoft.Extensions.Configuration;
using EcommerceLearn.Application.Interfaces.SMTP;
using EcommerceLearn.Domain.Entities.Users;
using EcommerceLearn.Domain.Entities.Auth;
using EcommerceLearn.Application.Emails;
using EcommerceLearn.Application.Interfaces.Security;

namespace EcommerceLearn.Application.Features.Auth.Services;

public sealed class AuthEmailService : IAuthEmailService
{
    private readonly IEmailTemplatesService _template;
    private readonly IConfiguration _config;
    private readonly IEmailService _email;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthEmailService(
        IEmailTemplatesService template,
        IConfiguration config,
        IEmailService email,
        IJwtTokenService jwtTokenService
    )
    {
        _template = template;
        _config = config;
        _email = email;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<EmailVerification> SendEmailVerification(User user)
    {
        var verification = EmailVerification.Create();


        var verificationLink = $"{_config["App:FrontendUrl"]}/auth/verification/{user.Email.Value}/{verification.Code}";

        var template = new EmailVerificationModel(
            verificationLink,
            user.FirstName,
            user.LastName,
            verification.Code
        );

        var emailHtml = await _template.GenerateEmailAsync("Auth/EmailVerification.cshtml", template);

        await _email.SendEmail("Email verification", emailHtml, user.Email.Value);

        return verification;
    }

    public async Task<PasswordVerification> SendPasswordVerification(User user)
    {
        var verification = PasswordVerification.Create();

        // Generate password reset link
        var token = _jwtTokenService.GeneratePasswordResetToken(user);

        var verificationLink = $"{_config["App:FrontendUrl"]}/auth/reset-password/{token}";

        var template = new ResetPasswordModel(
            verificationLink,
            user.FirstName
        );

        var emailHtml = await _template.GenerateEmailAsync("Auth/PasswordReset.cshtml", template);

        await _email.SendEmail("Reset Password", emailHtml, user.Email.Value);

        return verification;
    }
}