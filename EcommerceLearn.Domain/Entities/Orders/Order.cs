using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Users;
using EcommerceLearn.Domain.Enums.Orders;
using EcommerceLearn.Domain.ValueObjects;

namespace EcommerceLearn.Domain.Entities.Orders;

public sealed class Order : Entity<int>
{
    public int UserId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public Address ShippingAddress { get; private set; }
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public OrderStatus Status { get; private set; }

    public User User { get; private set; } = null!;

    private Order()
    {
    }

    private Order(int userId, Address shippingAddress)
    {
        UserId = userId;
        ShippingAddress = shippingAddress;
        Status = OrderStatus.Pending;
        TotalAmount = 0;
    }

    public static Result<Order> Create(int userId, string street, string city, string state, string country,
        string zipCode)
    {
        var address = Address.Create(street, city, state, country, zipCode);

        return Result<Order>.Success(new Order(userId, address));
    }

    public void AddOrderItem(OrderItem item)
    {
        if (item == null)
            return;

        var existing = _orderItems.FirstOrDefault(oi => oi.BookId == item.BookId);
        if (existing != null)
            existing.IncreaseQuantity(item.Quantity);
        else
            _orderItems.Add(item);

        RecalculateTotal();
    }

    public void RemoveOrderItem(int bookId, int quantity)
    {
        if (quantity <= 0)
            return;

        var item = _orderItems.FirstOrDefault(oi => oi.BookId == bookId);
        if (item == null)
            return;

        if (quantity >= item.Quantity)
            _orderItems.Remove(item);
        else
            item.IncreaseQuantity(-quantity);

        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        TotalAmount = _orderItems.Sum(oi => oi.Price);
    }

    public void SetShippingAddress(Address address)
    {
        if (address == null)
            return;

        ShippingAddress = address;
    }

    public void SetStatus(OrderStatus status)
    {
        Status = status;
    }
}