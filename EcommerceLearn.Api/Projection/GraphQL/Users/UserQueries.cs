using EcommerceLearn.Application.Features.Users.Queries.GetAllUsers;
using EcommerceLearn.Application.Features.Users.Queries.GetUserById;
using EcommerceLearn.Domain.Entities.Users;
using MediatR;

namespace EcommerceLearn.Api.Projection.GraphQL.Users;

[QueryType]
public sealed class UserQueries
{
    public async Task<IQueryable<User>> Me(int userId, IMediator mediator, CancellationToken ct)
    {
        return await mediator.Send(new GetUserByIdQueryable(userId), ct);
    }

    public async Task<List<User>> GetAllUsers(IMediator mediator, CancellationToken ct)
    {
        return await mediator.Send(new GetAllUsersQuery(), ct);
    }
}