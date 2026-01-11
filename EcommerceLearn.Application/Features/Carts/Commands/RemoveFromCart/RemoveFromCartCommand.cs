using MediatR;
using EcommerceLearn.Domain.Common.Results;

namespace EcommerceLearn.Application.Features.Carts.Commands.RemoveFromCart;

public sealed record RemoveFromCartCommand(int UserId, int BookId, int QuantityToRemove) : IRequest<Result>;