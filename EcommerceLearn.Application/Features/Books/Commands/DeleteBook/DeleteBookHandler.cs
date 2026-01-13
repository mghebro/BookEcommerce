using EcommerceLearn.Application.Features.Books.Queries.GetBookById;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace EcommerceLearn.Application.Features.Books.Commands.DeleteBook;

public sealed class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Result>
{
    private readonly IDataContext _db;
    private readonly IMediator _mediator;

    public DeleteBookHandler(IDataContext db, IMediator mediator)
    {
        _db = db;
        _mediator = mediator;
    }


    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken ct)
    {
        var bookQuery = await _mediator.Send(new GetBookByIdQueryable(request.Id), ct);
        var book = await bookQuery.FirstOrDefaultAsync(ct);
        if (book == null)
            return Result.Failure(Errors.NotFound("Book not found"));

        book.Delete();
        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }
}