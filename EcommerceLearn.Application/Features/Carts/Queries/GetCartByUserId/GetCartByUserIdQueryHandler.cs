using MediatR;
using EcommerceLearn.Domain.Entities.Carts;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Carts.Queries.GetCartByUserId;

public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, Result<Cart>>
{
    private readonly IDataContext _context;

    public GetCartByUserIdQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<Result<Cart>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Book)
            .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

        if (cart == null) return Result<Cart>.Failure(Errors.NotFound("Cart not found for this user."));

        return Result<Cart>.Success(cart);
    }
}