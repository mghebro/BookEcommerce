namespace EcommerceLearn.Application.Contracts.Users.Requests;

public sealed class DeleteAddressRequest
{
    public int UserId { get; set; }
    public int AddressId { get; set; }
}