namespace EcommerceLearn.Application.Contracts.Auth;

public sealed class ResendEmailCode
{
    public string Email { get; set; } = string.Empty;
}