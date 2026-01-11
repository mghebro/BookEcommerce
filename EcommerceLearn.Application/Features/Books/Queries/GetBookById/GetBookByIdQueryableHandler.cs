using EcommerceLearn.Application.Features.Books.Queries.GetBookById;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Books.Queries.GetBookById;

public sealed class GetBookByIdQueryableHandler : IRequestHandler<GetBookByIdQueryable, Result<Book>>
{
    private readonly IDataContext _db;

    public GetBookByIdQueryableHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Result<Book>> Handle(GetBookByIdQueryable req, CancellationToken ct)
    {
        var book = await _db.Books.FirstOrDefaultAsync(e => e.Id == req.Id);
        if (book == null)
            return Result<Book>.Failure(Errors.NotFound("User not found"));
        return Result<Book>.Success(book);
    }
}