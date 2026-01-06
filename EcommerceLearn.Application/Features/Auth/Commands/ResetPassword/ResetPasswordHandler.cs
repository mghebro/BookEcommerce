using EcommerceLearn.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using EcommerceLearn.Domain.Common.Results;
using FluentValidation;
using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.ResetPassword;

public sealed class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, Result<bool>>
{
    private readonly IDataContext _db;

    public ResetPasswordHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(ResetPasswordCommand req, CancellationToken ct)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Email.Value == req.Email, ct);

        if (user is null)
            throw new ValidationException(
                "Something went wrong with the password reset process. Please try again.");

        var verification = await _db.PasswordVerifications
            .FirstOrDefaultAsync(e => e.UserId == user.Id, ct);

        if (verification is null)
            throw new ValidationException(
                "Something went wrong with the password reset process. Please try again.");

        if (verification.Attempts >= 3)
            return Result<bool>.Failure(Errors.Invalid(
                "You have exceeded the allowed number of attempts. Please request a new code."));

        if (verification.ExpiredAt < DateTime.UtcNow)
            return Result<bool>.Failure(Errors.Invalid(
                "Your verification code has expired. Please request a new password reset."));

        _db.PasswordVerifications.Remove(verification);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
        user.SetPassword(hashedPassword);

        user.Verify();

        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}