using MediatR;
using EcommerceLearn.Domain.Entities.Orders;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Orders.Commands.PlaceOrder;

public sealed class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, Result>
{
    private readonly IDataContext _context;

    public PlaceOrderCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(PlaceOrderCommand request, CancellationToken ct)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Book)
            .FirstOrDefaultAsync(c => c.UserId == request.UserId, ct);

        if (cart == null || !cart.CartItems.Any())
            return Result.Failure(Errors.Invalid("Cart is empty or not found."));

        var orderResult = Order.Create(request.UserId, request.ShippingAddress);
        if (!orderResult.IsSuccess)
            return Result.Failure(orderResult.Error!);

        var order = orderResult.Value!;

        foreach (var cartItem in cart.CartItems)
        {
            if (cartItem.Book == null)
                return Result.Failure(Errors.NotFound($"Book with ID {cartItem.BookId} not found."));

            var orderItemResult = OrderItem.Create(cartItem.Book, cartItem.Quantity);
            if (!orderItemResult.IsSuccess)
                return Result.Failure(orderItemResult.Error!);

            var addResult = order.AddOrderItem(orderItemResult.Value!);
            if (!addResult.IsSuccess)
                return Result.Failure(addResult.Error!);
        }

        _context.Orders.Add(order);

        cart.ClearCart();

        await _context.SaveChangesAsync(ct);

        return Result.Success();
    }
}