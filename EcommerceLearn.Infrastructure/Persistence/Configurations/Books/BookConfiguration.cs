using EcommerceLearn.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommerceLearn.Infrastructure.Persistence.Configurations.Books;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(b => b.Isbn)
            .IsRequired()
            .HasMaxLength(17);
        builder.HasIndex(b => b.Isbn).IsUnique();

        builder.Property(b => b.PageCount)
            .IsRequired();

        builder.Property(b => b.CoverImageUrl)
            .HasMaxLength(500);

        builder.Property(b => b.AuthorFullname)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Language)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(b => b.Price)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}