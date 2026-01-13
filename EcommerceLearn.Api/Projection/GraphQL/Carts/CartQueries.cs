using System.Security.Claims;
using EcommerceLearn.Application.Features.Carts.Queries.GetCartByUserId;
using EcommerceLearn.Domain.Entities.Carts;
using HotChocolate.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Api.Projection.GraphQL.Carts;

[QueryType]
public sealed class CartQueries
{
    [Authorize]
    public async Task<IQueryable<Cart>> MyCart(
        IMediator mediator,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
            throw new GraphQLException("Invalid user ID");

        var cartQuery = await mediator.Send(new GetCartByUserIdQuery(userId), ct);


        return cartQuery;
    }
}