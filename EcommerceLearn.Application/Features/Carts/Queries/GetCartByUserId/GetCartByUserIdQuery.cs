using MediatR;
using EcommerceLearn.Domain.Entities.Carts;

namespace EcommerceLearn.Application.Features.Carts.Queries.GetCartByUserId;

public sealed record GetCartByUserIdQuery(int UserId) : IRequest<IQueryable<Cart>>;