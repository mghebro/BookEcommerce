using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Enums.Auth;
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
            request.AuthorFullname,
            request.Language,
            request.Price
        );

        if (!bookResult.IsSuccess)
            return Result.Failure(bookResult.Error!);

        var book = bookResult.Value!;

        if (!string.IsNullOrWhiteSpace(request.CoverImageUrl))
        {
            var coverResult = book.SetCoverImage(request.CoverImageUrl);
            if (!coverResult.IsSuccess)
                return Result.Failure(coverResult.Error!);
        }

        if (request.UserId.HasValue)
        {
            var user =
                await _db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId.Value,
                    cancellationToken);

            if (user == null)
                return Result.Failure(Errors.NotFound("user not found"));
            if (user.Role == UserRole.Author)
            {
                book.SetAuthor(request.UserId.Value, user);
                book.SetPublisher(request.UserId.Value, user, request.AuthorFullname);
            }

            if (user.Role == UserRole.Publisher)
                book.SetPublisher(request.UserId.Value, user, request.AuthorFullname);
        }


        if (request.Categories != null && request.Categories.Any())
            foreach (var category in request.Categories)
            {
                var categoryResult = book.AddCategory(category);
                if (!categoryResult.IsSuccess)
                    return Result.Failure(categoryResult.Error!);
            }

        _db.Books.Add(book);
        await _db.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}