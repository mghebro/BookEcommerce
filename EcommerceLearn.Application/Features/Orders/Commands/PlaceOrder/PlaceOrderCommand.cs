using MediatR;
using EcommerceLearn.Domain.ValueObjects;

namespace EcommerceLearn.Application.Features.Orders.Commands.PlaceOrder;

public sealed record PlaceOrderCommand(int UserId, Address ShippingAddress) : IRequest<Result>;