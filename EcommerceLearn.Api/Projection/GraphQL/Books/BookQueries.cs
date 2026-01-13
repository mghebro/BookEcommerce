using EcommerceLearn.Application.Features.Books.Queries.GetBookById;
using EcommerceLearn.Application.Features.Books.Queries.GetAllBooks;
using EcommerceLearn.Domain.Entities.Books;
using MediatR;

namespace EcommerceLearn.Api.Projection.GraphQL.Books;

[QueryType]
public sealed class BookQueries
{
    [UseProjection]
    public async Task<IQueryable<Book>> GetBookById(int id, IMediator mediator, CancellationToken ct)
    {
        return await mediator.Send(new GetBookByIdQueryable(id), ct);
    }

    [UseProjection]
    public async Task<IQueryable<Book>> GetAllBooks(IMediator mediator, CancellationToken ct)
    {
        return await mediator.Send(new GetAllBooksQuery(), ct);
    }
}