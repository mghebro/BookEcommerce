using EcommerceLearn.Application.Features.Books.Queries.GetBookById;
using EcommerceLearn.Application.Features.Carts.Queries.GetCartByUserId;
using MediatR;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Carts.Commands.AddToCart;

public sealed class AddToCartCommandHandler
    : IRequestHandler<AddToCartCommand, Result>
{
    private readonly IDataContext _context;
    private readonly IMediator _mediator;

    public AddToCartCommandHandler(IDataContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Result> Handle(AddToCartCommand request, CancellationToken ct)
    {
        var cartQuery = await _mediator.Send(new GetCartByUserIdQuery(request.UserId), ct);
        var cart = await cartQuery.FirstOrDefaultAsync(ct);

        if (cart == null)
            return Result.Failure(Errors.NotFound("Cart not found"));

        if (request.Quantity <= 0)
            return Result.Failure(Errors.ValidationFailed());

        cart.AddBook(request.BookId, request.Quantity);

        await _context.SaveChangesAsync(ct);
        return Result.Success();
    }
}