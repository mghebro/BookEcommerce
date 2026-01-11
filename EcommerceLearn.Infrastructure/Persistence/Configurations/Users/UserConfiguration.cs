using EcommerceLearn.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Infrastructure.Persistence.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ComplexProperty(x => x.Email);
        builder.HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}