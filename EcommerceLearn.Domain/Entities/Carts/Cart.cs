using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Domain.Entities.Carts;

public sealed class Cart : Entity<int>
{
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems;

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

    public void AddBook(int bookId, int quantity)
    {
        if (quantity <= 0)
            return;

        var existingItem = _cartItems.FirstOrDefault(ci => ci.BookId == bookId);

        if (existingItem == null)
        {
            var cartItem = CartItem.Create(bookId, quantity);
            _cartItems.Add(cartItem.Value!);
        }
        else
        {
            existingItem.IncreaseQuantity(quantity);
        }
    }


    public void RemoveBook(int bookId, int quantityToRemove)
    {
        if (quantityToRemove <= 0)
            return;

        var item = _cartItems.FirstOrDefault(ci => ci.BookId == bookId);
        if (item == null)
            return;

        if (quantityToRemove >= item.Quantity)
            _cartItems.Remove(item);
        else
            item.DecreaseQuantity(quantityToRemove);
    }


    public void ClearCart()
    {
        _cartItems.Clear();
    }
}