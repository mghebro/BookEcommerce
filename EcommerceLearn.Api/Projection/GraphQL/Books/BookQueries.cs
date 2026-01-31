using EcommerceLearn.Application.Features.Books.Queries.GetAllBooks;
using EcommerceLearn.Application.Features.Books.Queries.GetBookById;
using EcommerceLearn.Domain.Entities.Books;
using HotChocolate.CostAnalysis.Types;
using MediatR;

namespace EcommerceLearn.Api.Projection.GraphQL.Books;

[QueryType]
public sealed class BookQueries
{
    public async Task<IQueryable<Book>> GetBookById(int id, IMediator mediator, CancellationToken ct)
    {
        return await mediator.Send(new GetBookByIdQueryable(id), ct);
    }

    [UsePaging(IncludeTotalCount = true)]
    [UseFiltering(typeof(BookFilterType))]
    [UseSorting(typeof(BookSortType))]
    public async Task<IQueryable<Book>> GetAllBooks(IMediator mediator, CancellationToken ct)
    {
        return await mediator.Send(new GetAllBooksQuery(), ct);
    }
}