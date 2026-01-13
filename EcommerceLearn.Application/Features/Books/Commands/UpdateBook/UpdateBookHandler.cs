using EcommerceLearn.Application.Features.Books.Queries.GetBookById;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
{
    private readonly IDataContext _db;
    private readonly IMediator _mediator;

    public UpdateBookCommandHandler(IDataContext db, IMediator mediator)
    {
        _db = db;
        _mediator = mediator;
    }

    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken ct)
    {
        var bookQuery = await _mediator.Send(new GetBookByIdQueryable(request.Id), ct);
        var book = bookQuery.FirstOrDefaultAsync(ct);

        if (book == null)
            return Result.Failure(Errors.NotFound("Book not found"));

        book.Result!.Update(
            request.Title,
            request.Description,
            request.PageCount,
            request.CoverImageUrl,
            request.Language,
            request.Price
        );

        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }
}