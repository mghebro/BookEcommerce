using MediatR;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Books.Queries.GetAllBooks;

public sealed class GetAllBooksQueryHandler
    : IRequestHandler<GetAllBooksQuery, List<Book>>
{
    private readonly IDataContext _context;

    public GetAllBooksQueryHandler(IDataContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> Handle(
        GetAllBooksQuery request,
        CancellationToken ct)
    {
        return await _context.Books
            .AsNoTracking()
            .ToListAsync(ct);
    }
}