namespace EcommerceLearn.Domain.Common.Entities;

public class Entity<T>
{
    public T Id { get; set; }  = default!;
    
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } 
}