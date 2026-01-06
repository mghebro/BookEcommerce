using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Books.Commands.AddBookCategory;

public sealed class AddBookCategoryHandler : IRequestHandler<AddBookCategoryCommand, Result>
{
    private readonly IDataContext _db;

    public AddBookCategoryHandler(IDataContext db)
    {
        _db = db;
    }

    public async Task<Result> Handle(AddBookCategoryCommand request, CancellationToken cancellationToken)
    {
        var book = await _db.Books.Include(b => b.BookCategories)
            .FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken);

        if (book == null)
            return Result.Failure(Errors.NotFound("Book not found"));

        var result = book.AddCategory(request.Category);

        if (!result.IsSuccess)
            return result;

        await _db.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}