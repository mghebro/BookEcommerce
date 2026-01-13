using EcommerceLearn.Domain.Enums.Books;
using MediatR;

namespace EcommerceLearn.Application.Features.Books.Commands.CreateBook;

public sealed record CreateBookCommand(
    string Title,
    string Description,
    string Isbn,
    int PageCount,
    string CoverImageUrl,
    string AuthorFullname,
    string Language,
    decimal Price,
    List<Category>? Categories = null) : IRequest<Result>;