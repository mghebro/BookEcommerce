namespace EcommerceLearn.Application.Contracts.Auth;

public sealed class LoginRequest
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}