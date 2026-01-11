using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Books;

namespace EcommerceLearn.Domain.Entities.Carts;

public sealed class CartItem : Entity<int>
{
    public int Quantity { get; private set; }

    public int BookId { get; private set; }
    public Book Book { get; private set; } = null!;

    public int CartId { get; private set; }
    public Cart Cart { get; private set; } = null!;

    private CartItem()
    {
    }

    private CartItem(Book book, int quantity)
    {
        Book = book;
        BookId = book.Id;
        Quantity = quantity;
    }

    public static Result<CartItem> Create(Book book, int quantity)
    {
        if (quantity <= 0)
            return Result<CartItem>.Failure(
                Errors.Invalid("Quantity must be greater than 0"));

        return Result<CartItem>.Success(new CartItem(book, quantity));
    }

    public Result IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            return Result.Failure(
                Errors.Invalid("Quantity must be greater than 0"));

        Quantity += quantity;
        return Result.Success();
    }

    public Result DecreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            return Result.Failure(Errors.Invalid("Quantity must be greater than 0."));

        if (quantity > Quantity)
            return Result.Failure(Errors.Invalid("Cannot decrease more than current quantity."));

        Quantity -= quantity;
        return Result.Success();
    }
}