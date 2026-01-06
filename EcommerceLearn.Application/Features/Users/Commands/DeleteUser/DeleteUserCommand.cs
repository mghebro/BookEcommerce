using MediatR;

namespace EcommerceLearn.Application.Features.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(int UserId) : IRequest<Result>;
