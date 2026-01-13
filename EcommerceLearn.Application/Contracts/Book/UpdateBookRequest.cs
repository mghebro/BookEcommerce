namespace EcommerceLearn.Application.Contracts.Book;

public sealed class UpdateBookRequest
{
    public int Id { get; set; }
    public string? Title { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public int? PageCount { get; set; } = null!;
    public string? CoverImageUrl { get; set; } = null!;
    public string? Language { get; set; } = null!;
    public decimal? Price { get; set; } = null!;
}