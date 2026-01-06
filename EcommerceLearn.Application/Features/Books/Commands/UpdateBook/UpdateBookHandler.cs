using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EcommerceLearn.Application.Features.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
{
    private readonly IDataContext _db;

    public UpdateBookCommandHandler(IDataContext db)
    {
        _db = db;
    }


    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (book == null)
            return Result.Failure(Errors.NotFound("Book not found"));

        var updateResult = book.Update(
            request.Title,
            request.Description,
            request.PageCount,
            request.Language,
            request.Price
        );

        if (!updateResult.IsSuccess)
            return updateResult;

        if (!string.IsNullOrWhiteSpace(request.CoverImageUrl))
        {
            var coverResult = book.SetCoverImage(request.CoverImageUrl);
            if (!coverResult.IsSuccess)
                return coverResult;
        }

        await _db.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}