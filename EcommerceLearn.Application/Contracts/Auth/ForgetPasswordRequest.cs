namespace EcommerceLearn.Application.Contracts.Auth;

public sealed class ForgetPasswordRequest
{
    public string Email { get; set; } = string.Empty;
}