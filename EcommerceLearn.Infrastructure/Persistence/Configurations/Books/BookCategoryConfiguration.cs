using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Enums.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceLearn.Infrastructure.Persistence.Configurations.Books;

public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
{
    public void Configure(EntityTypeBuilder<BookCategory> builder)
    {
        builder.ToTable("BookCategories");

        builder.HasKey(bc => new { bc.BookId, bc.Category });

        builder.Property(bc => bc.Category)
            .IsRequired()
            .HasConversion<string>();


        builder.HasOne(bc => bc.Book)
            .WithMany(b => b.BookCategories)
            .HasForeignKey(bc => bc.BookId);
    }
}