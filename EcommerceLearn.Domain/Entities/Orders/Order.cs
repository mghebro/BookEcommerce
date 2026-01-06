using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Entities.Users;
using EcommerceLearn.Domain.Enums.Orders;
using EcommerceLearn.Domain.ValueObjects;

namespace EcommerceLearn.Domain.Entities.Orders;

public sealed class Order : Entity<int>
{
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public Address ShippingAddress { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public OrderStatus Status { get; set; }
    public User User { get; set; }
   
}