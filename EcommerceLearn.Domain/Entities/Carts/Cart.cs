using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Domain.Entities.Carts;

public sealed class Cart : Entity<int>
{
    public int UserId { get; private set; }

    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public User User { get; private set; } = null!;

    private Cart()
    {
    }

    private Cart(User user)
    {
        User = user;
        UserId = user.Id;
    }

    public static Cart Create(User user)
    {
        return new Cart(user);
    }

    public Result AddBook(Book book, int quantity)
    {
        var existingItem = _cartItems
            .FirstOrDefault(ci => ci.BookId == book.Id);

        if (existingItem is null)
        {
            var cartItemResult = CartItem.Create(book, quantity);
            if (!cartItemResult.IsSuccess)
                return cartItemResult;

            _cartItems.Add(cartItemResult.Value!);
        }
        else
        {
            var increaseResult = existingItem.IncreaseQuantity(quantity);
            if (!increaseResult.IsSuccess)
                return increaseResult;
        }

        return Result.Success();
    }

    public Result RemoveBook(int bookId, int quantityToRemove)
    {
        var item = _cartItems.FirstOrDefault(ci => ci.BookId == bookId);
        if (item is null)
            return Result.Failure(Errors.NotFound("Book not found in cart."));

        if (quantityToRemove <= 0)
            return Result.Failure(Errors.Invalid("Quantity to remove must be greater than 0."));

        if (quantityToRemove >= item.Quantity)
        {
            _cartItems.Remove(item);
        }
        else
        {
            var decreaseResult = item.DecreaseQuantity(quantityToRemove);
            if (!decreaseResult.IsSuccess)
                return decreaseResult;
        }

        return Result.Success();
    }

    public Result ClearCart()
    {
        _cartItems.Clear();
        return Result.Success();
    }
}