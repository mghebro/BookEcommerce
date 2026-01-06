using EcommerceLearn.Domain.Common.Results;

namespace EcommerceLearn.Domain.Common.Guards;

public static class Guard
{
    public static Result AgainstNullOrEmpty(string? value, Errors errors) =>
        string.IsNullOrWhiteSpace(value) ? Result.Failure(errors) : Result.Success();

    public static Result AgainstRegex(string value, string pattern, Errors errors) =>
        !System.Text.RegularExpressions.Regex.IsMatch(value, pattern)
            ? Result.Failure(errors) : Result.Success();

    public static Result AgainstStringRange(string value, int min, int max, Errors errors)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure(errors);
        
        return (value.Length < min || value.Length > max)
            ? Result.Failure(errors) : Result.Success();
    }

    public static Result AgainstOutOfRange(int value,int min, int max, Errors errors)
    {
        return (value < min || value > max)
            ? Result.Failure(errors)
            : Result.Success();
    }


    public static Result AgainstEqualsStringLength(string value, int exactLength, Errors errors)
    {
        if (string.IsNullOrWhiteSpace(value)) return Result.Failure(errors);
    
        return (value.Length != exactLength)
            ? Result.Failure(errors) : Result.Success();
    }

}