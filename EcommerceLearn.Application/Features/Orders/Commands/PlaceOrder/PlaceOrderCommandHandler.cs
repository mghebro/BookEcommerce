using EcommerceLearn.Application.Features.Carts.Queries.GetCartByUserId;
using MediatR;
using EcommerceLearn.Domain.Entities.Orders;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Orders.Commands.PlaceOrder;

public sealed class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Result>
{
    private readonly IDataContext _context;
    private readonly IMediator _mediator;

    public PlaceOrderCommandHandler(IDataContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Result> Handle(PlaceOrderCommand request, CancellationToken ct)
    {
        var cartQuery = await _mediator.Send(new GetCartByUserIdQuery(request.UserId), ct);
        var cart = await cartQuery.FirstOrDefaultAsync(ct);

        var orderResult = Order.Create(request.UserId, request.Street, request.City, request.State, request.Country,
            request.ZipCode);
        if (!orderResult.IsSuccess)
            return Result.Failure(orderResult.Error!);

        var order = orderResult.Value!;

        foreach (var cartItem in cart.CartItems)
        {
            var orderItem = OrderItem.FromCartItem(cartItem);
            order.AddOrderItem(orderItem);
        }

        _context.Orders.Add(order);
        cart.ClearCart();

        await _context.SaveChangesAsync(ct);

        return Result.Success();
    }
}