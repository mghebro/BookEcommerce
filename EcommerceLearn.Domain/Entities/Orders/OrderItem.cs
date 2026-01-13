using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Entities.Carts;

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


    public static OrderItem FromCartItem(CartItem cartItem)
    {
        return new OrderItem(cartItem.Book, cartItem.Quantity);
    }

    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            return;

        Quantity += quantity;
        Price = Book.Price * Quantity;
    }
}