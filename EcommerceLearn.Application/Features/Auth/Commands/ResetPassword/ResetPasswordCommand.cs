using MediatR;

namespace EcommerceLearn.Application.Features.Auth.Commands.ResetPassword;

public sealed record ResetPasswordCommand(
    string Email,
    string NewPassword
) : IRequest<Result<bool>>;