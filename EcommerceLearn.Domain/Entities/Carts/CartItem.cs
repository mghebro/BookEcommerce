using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Entities.Books;

namespace EcommerceLearn.Domain.Entities.Carts;

public sealed class CartItem : Entity<int>
{   
    
    public int Quantity { get; set; }
    
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    
    
}