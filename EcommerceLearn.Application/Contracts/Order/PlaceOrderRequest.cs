using EcommerceLearn.Domain.ValueObjects;

namespace EcommerceLearn.Application.Contracts.Order;

public sealed class PlaceOrderRequest
{
    public Address ShippingAddress { get; set; }
}