using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Application.Features.Auth.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.ForgetPassword;

public sealed class ForgetPasswordHandler : IRequestHandler<ForgetPasswordCommand, Result<bool>>
{
    private readonly IDataContext _db;
    private readonly IAuthEmailService _email;

    public ForgetPasswordHandler(
        IDataContext db,
        IAuthEmailService email
    )
    {
        _db = db;
        _email = email;
    }

    public async Task<Result<bool>> Handle(ForgetPasswordCommand req, CancellationToken ct)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Email.Value == req.Email, ct);

        if (user is null)
            throw new ValidationException(
                "No account found with this email address. Please check your email or sign up for a new account.");

        var existingVerification = await _db.PasswordVerifications
            .FirstOrDefaultAsync(e => e.UserId == user.Id, ct);

        if (existingVerification is not null)
            _db.PasswordVerifications.Remove(existingVerification);

        var passwordResetVerification = await _email.SendPasswordVerification(user);
        passwordResetVerification.SetUserId(user.Id);

        _db.PasswordVerifications.Add(passwordResetVerification);
        await _db.SaveChangesAsync(ct);

        return Result<bool>.Success(true);
    }
}