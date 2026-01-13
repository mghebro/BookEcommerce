using EcommerceLearn.Application.Contracts.Order;
using MediatR;
using EcommerceLearn.Domain.ValueObjects;

namespace EcommerceLearn.Application.Features.Orders.Commands.PlaceOrder;

public sealed record PlaceOrderCommand(
    int UserId,
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode) : IRequest<Result>;