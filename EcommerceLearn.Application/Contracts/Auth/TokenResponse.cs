namespace EcommerceLearn.Application.Contracts.Auth;

public sealed record TokenResponse(string AccessToken, string RefreshToken);