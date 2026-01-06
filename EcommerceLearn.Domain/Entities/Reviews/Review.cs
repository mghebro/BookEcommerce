using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Domain.Entities.Reviews;

public sealed class Review : Entity<int>
{
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}