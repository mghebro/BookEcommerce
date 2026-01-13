using EcommerceLearn.Application.Features.Books.Queries.GetBookById;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Entities.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Books.Queries.GetBookById;

public sealed class GetBookByIdQueryableHandler : IRequestHandler<GetBookByIdQueryable, IQueryable<Book>>
{
    private readonly IDataContext _db;

    public GetBookByIdQueryableHandler(IDataContext db)
    {
        _db = db;
    }

    public Task<IQueryable<Book>> Handle(GetBookByIdQueryable req, CancellationToken ct)
    {
        var query = _db.Books
            .Where(b => b.Id == req.Id && !b.IsDeleted)
            .AsNoTracking();

        return Task.FromResult(query);
    }
}