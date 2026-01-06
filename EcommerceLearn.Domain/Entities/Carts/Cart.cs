using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Domain.Entities.Carts;

public sealed class Cart : Entity<int>
{
    public int UserId { get; set; }
    public List<CartItem> CartItems { get; set; }
    public User User { get; set; }
}