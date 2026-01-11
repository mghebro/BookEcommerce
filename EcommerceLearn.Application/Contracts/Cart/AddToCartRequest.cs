namespace EcommerceLearn.Application.Contracts.Cart;

public sealed class AddToCartRequest
{
    public int BookId { get; set; }
    public int Quantity { get; set; } = 1;
}