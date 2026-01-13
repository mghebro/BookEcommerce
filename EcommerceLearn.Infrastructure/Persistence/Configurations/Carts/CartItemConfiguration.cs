using EcommerceLearn.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceLearn.Infrastructure.Persistence.Configurations.Carts;

public sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BookId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();
        builder.HasOne(x => x.Book)
            .WithMany()
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Cart>()
            .WithMany(c => c.CartItems)
            .HasForeignKey("CartId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}