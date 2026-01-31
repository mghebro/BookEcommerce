using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Common.Guards;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Domain.Entities.Addresses;

public sealed class UserAddress : Entity<int>
{
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public string Country { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string PostalCode { get; private set; } = string.Empty;

    public bool IsDefault { get; private set; } = false;
    public bool IsDeleted { get; private set; } = false;

    private UserAddress()
    {
    }

    private UserAddress(
        int userId,
        string country,
        string city,
        string street,
        string postalCode
    )
    {
        UserId = userId;
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public static Result<UserAddress> Create(
        int userId,
        string country,
        string city,
        string street,
        string postalCode
    )
    {
        var countryResult = Guard.AgainstStringRange(country, 2, 60,
            Errors.Invalid("Country must be between 2 and 60 characters"));
        if (!countryResult.IsSuccess) return Result<UserAddress>.Failure(countryResult.Error!);

        var cityResult = Guard.AgainstStringRange(city, 2, 60,
            Errors.Invalid("City must be between 2 and 60 characters"));
        if (!cityResult.IsSuccess) return Result<UserAddress>.Failure(cityResult.Error!);

        var streetResult = Guard.AgainstStringRange(street, 2, 100,
            Errors.Invalid("Street must be between 2 and 100 characters"));
        if (!streetResult.IsSuccess) return Result<UserAddress>.Failure(streetResult.Error!);

        var postalResult = Guard.AgainstStringRange(postalCode, 2, 20,
            Errors.Invalid("Postal code must be between 2 and 20 characters"));
        if (!postalResult.IsSuccess) return Result<UserAddress>.Failure(postalResult.Error!);

        return Result<UserAddress>.Success(
            new UserAddress(userId, country, city, street, postalCode)
        );
    }

    public void Update(
        string? country,
        string? city,
        string? street,
        string? postalCode
    )
    {
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public void MarkAsDefault()
    {
        IsDefault = true;
    }

    public void UnmarkAsDefault()
    {
        IsDefault = false;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}