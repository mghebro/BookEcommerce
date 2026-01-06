namespace EcommerceLearn.Application.Contracts.Auth;

public sealed class VerifyEmail
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}