using FluentValidation;

namespace EcommerceLearn.Application.Features.Users.Commands.EditUser;

public sealed class EditUserValidator : AbstractValidator<EditUserCommand>
{
    public EditUserValidator()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(2).WithMessage("First name must be at least 2 characters")
            .MaximumLength(40).WithMessage("First name must not exceed 40 characters")
            .Matches("^[a-zA-Z]+$").WithMessage("First name can only contain letters");

        RuleFor(x => x.LastName)
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters")
            .MaximumLength(60).WithMessage("Last name must not exceed 60 characters")
            .Matches("^[a-zA-Z]+$").WithMessage("Last name can only contain letters");
    }
}