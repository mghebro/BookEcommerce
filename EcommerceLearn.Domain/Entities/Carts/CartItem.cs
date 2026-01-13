using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Books;

namespace EcommerceLearn.Domain.Entities.Carts;

public sealed class CartItem : Entity<int>
{
    public int BookId { get; private set; }
    public Book Book { get; private set; } = null!;
    public int Quantity { get; private set; }

    private CartItem()
    {
    } 

    private CartItem(int bookId, int quantity)
    {
        BookId = bookId;
        Quantity = quantity;
    }

    public static Result<CartItem> Create(int bookId, int quantity)
    {
        if (quantity <= 0)
            return Result<CartItem>.Failure(
                Errors.Invalid("Quantity must be greater than 0"));

        return Result<CartItem>.Success(
            new CartItem(bookId, quantity));
    }

    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0 == false)
            Quantity += quantity;
    }

    public void DecreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            return;

        if (quantity > Quantity)
            return;

        Quantity -= quantity;
    }
}