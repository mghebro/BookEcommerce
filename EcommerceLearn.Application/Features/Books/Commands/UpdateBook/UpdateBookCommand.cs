using MediatR;

namespace EcommerceLearn.Application.Features.Books.Commands.UpdateBook;

public sealed record UpdateBookCommand(
    int Id,
    string? Title = null,
    string? Description = null,
    int? PageCount = null,
    string? Language = null,
    decimal? Price = null,
    string? CoverImageUrl = null
) : IRequest<Result>;