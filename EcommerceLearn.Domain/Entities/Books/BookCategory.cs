using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Enums.Books;

namespace EcommerceLearn.Domain.Entities.Books;

public sealed class BookCategory
{
    public int BookId { get; private set; }
    public Book Book { get; private set; } = null!;

    public Category Category { get; private set; }

    private BookCategory()
    {
    }

    private BookCategory(int bookId, Book book, Category category)
    {
        BookId = bookId;
        Book = book;
        Category = category;
    }

    public static Result<BookCategory> Create(int bookId, Book book, Category category)
    {
        if (book == null)
            return Result<BookCategory>.Failure(Errors.Invalid("Book cannot be null"));

        if (bookId <= 0)
            return Result<BookCategory>.Failure(Errors.Invalid("BookId must be greater than 0"));

        return Result<BookCategory>.Success(new BookCategory(bookId, book, category));
    }
}