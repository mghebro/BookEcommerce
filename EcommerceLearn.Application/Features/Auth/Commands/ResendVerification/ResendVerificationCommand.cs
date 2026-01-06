using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.ResendVerification;

public sealed record ResendVerificationCommand(string Email) : IRequest<Result<bool>>;