namespace EcommerceLearn.Application.Contracts.Cart;

public sealed class RemoveFromCartRequest
{
    public int BookId { get; set; }

    public int QuantityToRemove { get; set; } = 1;
}