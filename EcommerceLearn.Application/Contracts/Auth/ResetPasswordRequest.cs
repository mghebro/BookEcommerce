namespace EcommerceLearn.Application.Contracts.Auth;

public sealed class ResetPasswordRequest
{
    public string Email { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}