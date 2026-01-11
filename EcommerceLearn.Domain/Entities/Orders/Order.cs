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

    public static Result<Order> Create(int userId, Address shippingAddress)
    {
        if (userId <= 0)
            return Result<Order>.Failure(Errors.Invalid("UserId must be greater than 0"));

        if (shippingAddress is null)
            return Result<Order>.Failure(Errors.Invalid("Shipping address cannot be null"));

        return Result<Order>.Success(new Order(userId, shippingAddress));
    }

    public Result AddOrderItem(OrderItem item)
    {
        if (item is null)
            return Result.Failure(Errors.Invalid("Order item cannot be null"));

        var existing = _orderItems.FirstOrDefault(oi => oi.BookId == item.BookId);
        if (existing != null)
        {
            var result = existing.IncreaseQuantity(item.Quantity);
            if (!result.IsSuccess) return result;
        }
        else
        {
            _orderItems.Add(item);
        }

        RecalculateTotal();
        return Result.Success();
    }

    public Result RemoveOrderItem(int bookId, int quantity)
    {
        var item = _orderItems.FirstOrDefault(oi => oi.BookId == bookId);
        if (item == null)
            return Result.Failure(Errors.NotFound("Order item not found"));

        if (quantity >= item.Quantity)
        {
            _orderItems.Remove(item);
        }
        else
        {
            var result = item.IncreaseQuantity(-quantity); // negative quantity
            if (!result.IsSuccess) return result;
        }

        RecalculateTotal();
        return Result.Success();
    }

    private void RecalculateTotal()
    {
        TotalAmount = _orderItems.Sum(oi => oi.Price);
    }

    public Result SetShippingAddress(Address address)
    {
        if (address == null)
            return Result.Failure(Errors.Invalid("Shipping address cannot be null"));

        ShippingAddress = address;
        return Result.Success();
    }

    public Result SetStatus(OrderStatus status)
    {
        Status = status;
        return Result.Success();
    }
}