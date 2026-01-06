using EcommerceLearn.Application.Features.Books.Queries.GetBookById;
using EcommerceLearn.Domain.Entities.Books;
using MediatR;

namespace EcommerceLearn.Api.Projection.GraphQL.Books;

[QueryType]
public sealed class BookQueries
{
    public async Task<IQueryable<Book>> GetBookById(GetBookByIdQueryable req, IMediator mediator, CancellationToken ct)
    {
        return await mediator.Send(new GetBookByIdQueryable(req.Id), ct);
    }
}