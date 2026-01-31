namespace EcommerceLearn.Application.Contracts.Users.Requests;

public sealed class EditAddressRequest
{
    public int UserId { get; set; }
    public int AddressId { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public bool IsDefault { get; set; } = false;
}