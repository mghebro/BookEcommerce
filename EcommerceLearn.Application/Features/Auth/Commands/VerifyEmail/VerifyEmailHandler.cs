using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Application.Interfaces.Security;
using EcommerceLearn.Application.Contracts.Auth;
using Microsoft.EntityFrameworkCore;
using EcommerceLearn.Domain.Common.Results;
using FluentValidation;
using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.VerifyEmail;

public sealed class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, Result<TokenResponse>>
{
    private readonly IDataContext _db;
    private readonly IJwtTokenService _jwt;

    public VerifyEmailHandler(
        IDataContext db,
        IJwtTokenService jwt
    )
    {
        _db = db;
        _jwt = jwt;
    }

    public async Task<Result<TokenResponse>> Handle(VerifyEmailCommand req, CancellationToken ct)
    {
        var user = await _db.Users
            .Include(u => u.EmailVerification)
            .FirstOrDefaultAsync(u => u.Email.Value == req.Email, ct);

        if (user == null)
            throw new ValidationException("Unable to complete email verification. Please try again.");

        if (user.IsVerified)
            return Result<TokenResponse>.Failure(Errors.Invalid("This account has already been verified."));

        if (user.EmailVerification.Attempts >= 3)
            return Result<TokenResponse>.Failure(Errors.Invalid(
                "You have reached the maximum number of verification attempts. Please request a new code."));

        if (user.EmailVerification.Code != req.Code)
        {
            user.EmailVerification.IncrementAttempts();
            await _db.SaveChangesAsync(ct);

            return Result<TokenResponse>.Failure(Errors.Invalid("The verification code you entered is incorrect."));
        }

        if (user.EmailVerification.ExpiredAt < DateTime.UtcNow)
            return Result<TokenResponse>.Failure(
                Errors.Invalid("Your verification code has expired. Please request a new one."));

        user.EmailVerification.DeleteCode();
        user.Verify();

        await _db.SaveChangesAsync(ct);

        var accessToken = _jwt.GenerateAccessToken(user);
        var refreshToken = _jwt.GenerateAccessToken(user);

        return Result<TokenResponse>.Success(new TokenResponse(accessToken, refreshToken));
    }
}