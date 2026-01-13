using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Books;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Books.Commands.CreateBook;

public sealed class CreateBookHandler : IRequestHandler<CreateBookCommand, Result>
{
    private readonly IDataContext _db;

    public CreateBookHandler(IDataContext db)
    {
        _db = db;
    }


    public async Task<Result> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var bookResult = Book.Create(
            request.Title,
            request.Description,
            request.Isbn,
            request.PageCount,
            request.CoverImageUrl,
            request.AuthorFullname,
            request.Language,
            request.Price
        );

        if (!bookResult.IsSuccess)
            return Result.Failure(bookResult.Error!);

        var book = bookResult.Value!;

        if (request.Categories != null && request.Categories.Any())
            foreach (var category in request.Categories)
                book.AddCategory(category);

        _db.Books.Add(book);
        await _db.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}