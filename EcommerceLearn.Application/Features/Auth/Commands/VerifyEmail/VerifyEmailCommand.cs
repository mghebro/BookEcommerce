using EcommerceLearn.Application.Contracts.Auth;
using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.VerifyEmail;

public sealed record VerifyEmailCommand(
    string Email,
    string Code
) : IRequest<Result<TokenResponse>>;