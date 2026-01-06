using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.ForgetPassword;

public sealed record ForgetPasswordCommand(string Email) : IRequest<Result<bool>>;