using EcommerceLearn.Domain.Enums.Books;

namespace EcommerceLearn.Application.Contracts.Book;

public sealed class CreateBookRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int PageCount { get; set; }
    public string CoverImageUrl { get; set; } = string.Empty;
    public string AuthorFullname { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public List<Category> Categories { get; set; } = null!;
}