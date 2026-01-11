using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Common.Guards;
using EcommerceLearn.Domain.Entities.Auth;
using EcommerceLearn.Domain.Entities.Carts;
using EcommerceLearn.Domain.ValueObjects;

namespace EcommerceLearn.Domain.Entities.Users;

//sealed you can not change it from outside use it as it is
public sealed class User : Entity<int>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? Password { get; private set; } = string.Empty;

    public Email Email { get; private set; }
    public bool IsVerified { get; private set; } = false;
    public Cart Cart { get; private set; } = null!;
    public EmailVerification EmailVerification { get; private set; }
    public PasswordVerification PasswordEmailVerification { get; private set; }


    // Helps EF Core to create table 
    private User()
    {
    }

    private User(string firstName, string lastName, Email email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void CreateCart()
    {
        Cart = Cart.Create(this);
    }

    //factory method for create user it uses validations
    public static Result<User> Create(string firstName, string lastName, string email)
    {
        var firstNameResult = Guard.AgainstStringRange(firstName, 2, 40,
            Errors.Invalid("firstName must be between 4 and 60 characters"));
        if (!firstNameResult.IsSuccess) return Result<User>.Failure(firstNameResult.Error!);

        var lastNameResult = Guard.AgainstStringRange(lastName, 2, 40,
            Errors.Invalid("lastName must be between 4 and 60 characters"));
        if (!lastNameResult.IsSuccess) return Result<User>.Failure(lastNameResult.Error!);

        var emailResult = Email.Create(email);

        if (!emailResult.IsSuccess) return Result<User>.Failure(emailResult.Error!);

        return Result<User>.Success(new User(firstName, lastName, emailResult.Value!));
    }

    public void SetPassword(string password)
    {
        Password = password;
    }

    public void Rename(string? firstName, string? lastName)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
            FirstName = firstName;
        if (!string.IsNullOrWhiteSpace(lastName))
            LastName = lastName;
    }

    public void Verify()
    {
        IsVerified = true;
    }

    public void SetEmailVerification(EmailVerification verification)
    {
        EmailVerification = verification;
    }

    public void SetPasswordEmailVerification(PasswordVerification verification)
    {
        PasswordEmailVerification = verification;
    }
}