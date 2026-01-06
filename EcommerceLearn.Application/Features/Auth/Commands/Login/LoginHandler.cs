using EcommerceLearn.Application.Features.Auth.Services;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Application.Interfaces.Security;
using EcommerceLearn.Application.Contracts.Auth;
using Microsoft.EntityFrameworkCore;
using EcommerceLearn.Domain.Common.Results;
using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.Login;

public sealed class LoginHandler : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    private readonly IDataContext _db;
    private readonly IJwtTokenService _jwt;
    private readonly IAuthEmailService _email;

    public LoginHandler(
        IDataContext db,
        IJwtTokenService jwt,
        IAuthEmailService email
    )
    {
        _db = db;
        _jwt = jwt;
        _email = email;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand req, CancellationToken ct)
    {
        var user = await _db.Users.AsNoTracking()
            .Where(e => e.Email.Value == req.Email)
            .FirstOrDefaultAsync(ct);

        if (user is null)
            return Result<TokenResponse>.Failure(Errors.InvalidCredentials());

        if (!BCrypt.Net.BCrypt.Verify(req.Password, user.Password))
            return Result<TokenResponse>.Failure(Errors.InvalidCredentials());

        if (!user.IsVerified)
        {
            var verification = await _email.SendEmailVerification(user);
            verification.SetUserId(user.Id);

            var verificationExits = await _db.EmailVerifications
                .FirstOrDefaultAsync(e => e.UserId == user.Id, ct);

            if (verificationExits is not null) _db.EmailVerifications.Remove(verificationExits);

            _db.EmailVerifications.Add(verification);
            await _db.SaveChangesAsync(ct);

            return Result<TokenResponse>.Success(new TokenResponse("Verification", "Verification"));
        }

        var accessToken = _jwt.GenerateAccessToken(user);
        var refreshToken = _jwt.GenerateAccessToken(user);

        return Result<TokenResponse>.Success(new TokenResponse(accessToken, refreshToken));
    }
}