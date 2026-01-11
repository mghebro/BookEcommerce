using EcommerceLearn.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceLearn.Infrastructure.Persistence.Configurations.Orders;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UserId)
            .IsRequired();

        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(o => o.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.OwnsOne(o => o.ShippingAddress, sa =>
        {
            sa.Property(a => a.Country).HasMaxLength(100).IsRequired();
            sa.Property(a => a.City).HasMaxLength(100).IsRequired();
            sa.Property(a => a.Street).HasMaxLength(200).IsRequired();
            sa.Property(a => a.ZipCode).HasMaxLength(20).IsRequired();
        });

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(o => o.OrderItems)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}