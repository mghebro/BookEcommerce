using EcommerceLearn.Domain.Entities.Orders;
using MediatR;

namespace EcommerceLearn.Application.Features.Orders.Queries.GetUserOrders;

public sealed record GetUserOrdersQueryable(int UserId) : IRequest<IQueryable<Order>>;