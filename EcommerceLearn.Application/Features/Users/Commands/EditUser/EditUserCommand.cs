using MediatR;

namespace EcommerceLearn.Application.Features.Users.Commands.EditUser;

public sealed record EditUserCommand(
    string FirstName,
    string LastName,
    int UserId
) : IRequest<Result>;