using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Entities.Orders;
using MediatR;

namespace EcommerceLearn.Application.Features.Orders.Queries.GetUserOrders;

public sealed class GetUserOrdersQueryableHandler : IRequestHandler<GetUserOrdersQueryable, IQueryable<Order>>
{
    private readonly IDataContext _db;

    public GetUserOrdersQueryableHandler(IDataContext db)
    {
        _db = db;
    }

    public Task<IQueryable<Order>> Handle(GetUserOrdersQueryable request, CancellationToken cancellationToken)
    {
        var order = _db.Orders.Where(e => e.UserId == request.UserId);
        return Task.FromResult(order.AsQueryable());
    }
}