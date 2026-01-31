using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Addresses;
using EcommerceLearn.Domain.Entities.Users;
using EcommerceLearn.Domain.Enums.Orders;

namespace EcommerceLearn.Domain.Entities.Orders;

public sealed class Order : Entity<int>
{
    // Relations
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public OrderStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public UserAddress ShippingAddress { get; private set; }
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    private Order()
    {
        // Required for EF
    }

    private Order(int userId, UserAddress shippingAddress)
    {
        UserId = userId;
        ShippingAddress = shippingAddress;
        Status = OrderStatus.Pending;
        TotalAmount = 0;
    }

    public static Result<Order> Create(int userId, UserAddress shippingAddress)
    {
        if (shippingAddress == null)
            return Result<Order>.Failure(Errors.Invalid("Shipping address is Required"));

        var order = new Order(userId, shippingAddress);
        return Result<Order>.Success(order);
    }

    public void AddOrderItem(OrderItem item)
    {
        if (item == null) return;

        var existing = _orderItems.FirstOrDefault(oi => oi.BookId == item.BookId);
        if (existing != null)
            existing.IncreaseQuantity(item.Quantity);
        else
            _orderItems.Add(item);

        RecalculateTotal();
    }

    // Remove items
    public void RemoveOrderItem(int bookId, int quantity)
    {
        if (quantity <= 0) return;

        var item = _orderItems.FirstOrDefault(oi => oi.BookId == bookId);
        if (item == null) return;

        if (quantity >= item.Quantity)
            _orderItems.Remove(item);
        else
            item.IncreaseQuantity(-quantity);

        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        TotalAmount = _orderItems.Sum(oi => oi.Price * oi.Quantity);
    }


    public void SetStatus(OrderStatus status)
    {
        Status = status;
    }
}