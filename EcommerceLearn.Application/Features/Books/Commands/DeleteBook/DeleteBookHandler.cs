using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace EcommerceLearn.Application.Features.Books.Commands.DeleteBook;

public sealed class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Result>
{
    private readonly IDataContext _db;

    public DeleteBookHandler(IDataContext db)
    {
        _db = db;
    }


    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (book == null)
            return Result.Failure(Errors.NotFound("Book not found"));

        _db.Books.Remove(book);
        await _db.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}