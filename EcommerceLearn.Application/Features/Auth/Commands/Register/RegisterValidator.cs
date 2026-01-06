using EcommerceLearn.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace EcommerceLearn.Application.Features.Auth.Commands.Register;

public sealed class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator(IDataContext db)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .MinimumLength(4).WithMessage("FirstName must be at least 4 characters")
            .MaximumLength(60).WithMessage("FirstName must not exceed 60 characters");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .MinimumLength(4).WithMessage("LastName must be at least 4 characters")
            .MaximumLength(60).WithMessage("LastName must not exceed 60 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters")
            .MustAsync(async (email, ct) => !await db.Users.AnyAsync(u => u.Email.Value == email, ct))
            .WithMessage("Email already exists!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(128).WithMessage("Password must not exceed 128 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
    }
}