using EcommerceLearn.Domain.Common.Entities;

namespace EcommerceLearn.Domain.Entities.Orders;

public sealed class OrderItem : Entity<int>
{
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}