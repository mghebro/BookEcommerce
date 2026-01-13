using EcommerceLearn.Application.Features.Books.Queries.GetBookById;
using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Books.Commands.AddBookCategory;

public sealed class AddBookCategoryHandler : IRequestHandler<AddBookCategoryCommand, Result>
{
    private readonly IDataContext _db;
    private readonly IMediator _mediator;

    public AddBookCategoryHandler(IDataContext db, IMediator mediator)
    {
        _db = db;
        _mediator = mediator;
    }

    public async Task<Result> Handle(AddBookCategoryCommand request, CancellationToken ct)
    {
        var bookQuery = await _mediator.Send(new GetBookByIdQueryable(request.BookId), ct);

        var book = await bookQuery.FirstOrDefaultAsync(ct);

        if (book == null)
            return Result.Failure(Errors.NotFound("Book not found"));

        if (!book.BookCategories.Any(c => c.Category == request.Category))
            book.AddCategory(request.Category);

        await _db.SaveChangesAsync(ct);

        return Result.Success();
    }
}