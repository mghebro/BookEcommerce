namespace EcommerceLearn.Application.Contracts.Users.Requests;

public sealed class EditUserRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}