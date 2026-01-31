namespace EcommerceLearn.Application.Contracts.Cart;

public sealed class MergeGuestCartRequest
{
    public List<MergeGuestCartItem> Items { get; set; } = new();
}