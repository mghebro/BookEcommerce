namespace EcommerceLearn.Application.Contracts.Users.Requests;

public sealed class AddAddressRequest
{
    public int UserId { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}