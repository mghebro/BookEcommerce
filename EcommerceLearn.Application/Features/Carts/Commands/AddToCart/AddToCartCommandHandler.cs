using MediatR;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Carts.Commands.AddToCart;

public sealed class AddToCartCommandHandler
    : IRequestHandler<AddToCartCommand, Result>
{
    private readonly IDataContext _context;

    public AddToCartCommandHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        AddToCartCommand request,
        CancellationToken ct)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == request.UserId, ct);


        var book = await _context.Books
            .FirstOrDefaultAsync(b => b.Id == request.BookId, ct);

        if (book is null)
            return Result.Failure(
                Errors.NotFound("Book not found"));

        var addResult = cart.AddBook(book, request.Quantity);
        if (!addResult.IsSuccess)
            return addResult;

        await _context.SaveChangesAsync(ct);
        return Result.Success();
    }
}