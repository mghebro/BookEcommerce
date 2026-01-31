using System.Security.Claims;
using EcommerceLearn.Application.Features.Orders.Queries.GetUserOrders;
using EcommerceLearn.Domain.Entities.Orders;
using HotChocolate.Authorization;
using MediatR;

namespace EcommerceLearn.Api.Projection.GraphQL.Orders;

public sealed class OrderQueries
{
    [Authorize]
    public async Task<IQueryable<Order>> MyOrders(
        IMediator mediator,
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdClaim, out var userId))
            throw new GraphQLException("Invalid user ID");

        var ordersQuery = await mediator.Send(new GetUserOrdersQueryable(userId), ct);

        return ordersQuery;
    }
}