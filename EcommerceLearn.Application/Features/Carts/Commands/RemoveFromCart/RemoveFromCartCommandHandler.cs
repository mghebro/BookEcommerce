using MediatR;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Carts.Commands.RemoveFromCart;

public sealed class RemoveFromCartCommandHandler
    : IRequestHandler<RemoveFromCartCommand, Result>
{
    private readonly IDataContext _context;

    public RemoveFromCartCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        RemoveFromCartCommand request,
        CancellationToken ct)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == request.UserId, ct);

        if (cart == null)
            return Result.Failure(Errors.NotFound("Cart not found."));

        var removeResult = cart.RemoveBook(request.BookId, request.QuantityToRemove);
        if (!removeResult.IsSuccess)
            return removeResult;

        await _context.SaveChangesAsync(ct);
        return Result.Success();
    }
}