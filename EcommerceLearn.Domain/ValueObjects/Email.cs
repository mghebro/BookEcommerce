using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Common.Guards;
using EcommerceLearn.Domain.Common.Models;

namespace EcommerceLearn.Domain.ValueObjects;

public sealed class Email : ValueObject
{

    public string Value { get; private set; } = string.Empty;

    
    private Email() { }
    
    private Email(string value) => Value = value;

    // Factory method to create a valid Email object
    public static Result<Email> Create(string value)
    {
     
        var nullCheck = Guard.AgainstNullOrEmpty(value, Errors.Invalid("Email is required!"));
        if (!nullCheck.IsSuccess) 
            return Result<Email>.Failure(nullCheck.Error!);

 
        var formatCheck = Guard.AgainstRegex(
            value,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            Errors.Invalid("Invalid email format!")
        );
        if (!formatCheck.IsSuccess) 
            return Result<Email>.Failure(formatCheck.Error!);

     
        return Result<Email>.Success(new Email(value));
    }

    // Define equality for this Value Object
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}