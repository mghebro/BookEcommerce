using EcommerceLearn.Application.Contracts.Auth;
using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password
) : IRequest<Result<TokenResponse>>;