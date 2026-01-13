using MediatR;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Books.Queries.GetAllBooks;

public sealed class GetAllBooksQueryHandler
    : IRequestHandler<GetAllBooksQuery, IQueryable<Book>>
{
    private readonly IDataContext _context;

    public GetAllBooksQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public Task<IQueryable<Book>> Handle(GetAllBooksQuery request, CancellationToken ct)
    {
        var query = _context.Books
            .AsNoTracking()
            .Where(b => !b.IsDeleted);

        return Task.FromResult(query);
    }
}