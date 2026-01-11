using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Books;

namespace EcommerceLearn.Domain.Entities.Orders;

public sealed class OrderItem : Entity<int>
{
    public int OrderId { get; private set; }
    public int BookId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public Order Order { get; private set; } = null!;
    public Book Book { get; private set; } = null!;

    private OrderItem()
    {
    }

    private OrderItem(Book book, int quantity)
    {
        Book = book;
        BookId = book.Id;
        Quantity = quantity;
        Price = book.Price * quantity;
    }

    public static Result<OrderItem> Create(Book book, int quantity)
    {
        if (quantity <= 0)
            return Result<OrderItem>.Failure(
                Errors.Invalid("Quantity must be greater than 0"));

        if (book.Price < 0)
            return Result<OrderItem>.Failure(
                Errors.Invalid("Book price cannot be negative"));

        return Result<OrderItem>.Success(new OrderItem(book, quantity));
    }

    public Result IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            return Result.Failure(Errors.Invalid("Quantity must be greater than 0"));

        Quantity += quantity;
        Price = Book.Price * Quantity;
        return Result.Success();
    }
}