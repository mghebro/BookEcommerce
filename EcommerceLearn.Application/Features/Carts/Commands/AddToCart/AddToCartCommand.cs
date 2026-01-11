using MediatR;
using EcommerceLearn.Domain.Common.Results;

namespace EcommerceLearn.Application.Features.Carts.Commands.AddToCart;

public sealed record AddToCartCommand(int UserId, int BookId, int Quantity) : IRequest<Result>;