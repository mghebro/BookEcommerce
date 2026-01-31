using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using EcommerceLearn.Application.Features.Users.Queries.GetAllUsers;
using EcommerceLearn.Application.Features.Users.Queries.GetUserById;
using EcommerceLearn.Domain.Entities.Users;
using HotChocolate.Authorization;
using MediatR;

namespace EcommerceLearn.Api.Projection.GraphQL.Users;

[QueryType]
public sealed class UserQueries
{
    [Authorize]
    public async Task<IQueryable<User>> Me(
        IMediator mediator,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            throw new GraphQLException("Invalid user ID");

        var userQuery = await mediator.Send(new GetUserByIdQueryable(userId), ct);
        return userQuery;
    }

    public async Task<IQueryable<User>> GetAllUsers(IMediator mediator, CancellationToken ct)
    {
        return await mediator.Send(new GetAllUsersQuery(), ct);
    }
}