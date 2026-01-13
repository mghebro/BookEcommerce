using MediatR;
using EcommerceLearn.Domain.Entities.Carts;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Carts.Queries.GetCartByUserId;

public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, IQueryable<Cart>>
{
    private readonly IDataContext _context;

    public GetCartByUserIdQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public Task<IQueryable<Cart>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
    {
        var cartQuery = _context.Carts
            .Include(c => c.CartItems.Where(ci => !ci.Book.IsDeleted))
            .ThenInclude(ci => ci.Book)
            .Where(c => c.UserId == request.UserId);

        return Task.FromResult(cartQuery);
    }
}