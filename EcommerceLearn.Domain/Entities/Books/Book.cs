using EcommerceLearn.Domain.Common.Entities;
using EcommerceLearn.Domain.Common.Results;
using EcommerceLearn.Domain.Common.Guards;
using EcommerceLearn.Domain.Enums.Books;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Domain.Entities.Books;

public sealed class Book : Entity<int>
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Isbn { get; private set; } = string.Empty;
    public int PageCount { get; private set; }
    public string CoverImageUrl { get; private set; } = string.Empty;
    public string AuthorFullname { get; private set; } = string.Empty;


    private readonly List<BookCategory> _bookCategories = new();
    public IReadOnlyCollection<BookCategory> BookCategories => _bookCategories.AsReadOnly();

    public string Language { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    private Book()
    {
    }

    private Book(
        string title,
        string description,
        string isbn,
        int pageCount,
        string authorFullname,
        string language,
        decimal price)
    {
        Title = title;
        Description = description;
        Isbn = isbn;
        PageCount = pageCount;
        AuthorFullname = authorFullname;
        Language = language;
        Price = price;
    }

    public static Result<Book> Create(
        string title,
        string description,
        string isbn,
        int pageCount,
        string authorFullname,
        string language,
        decimal price)
    {
        var titleResult = Guard.AgainstStringRange(title, 1, 200,
            Errors.Invalid("Title must be between 1 and 200 characters"));

        if (!titleResult.IsSuccess) return Result<Book>.Failure(titleResult.Error!);

        var descriptionResult = Guard.AgainstStringRange(description, 10, 2000,
            Errors.Invalid("Description must be between 10 and 2000 characters"));

        if (!descriptionResult.IsSuccess) return Result<Book>.Failure(descriptionResult.Error!);

        var isbnResult = Guard.AgainstStringRange(isbn, 10, 17,
            Errors.Invalid("ISBN must be between 10 and 17 characters"));

        if (!isbnResult.IsSuccess) return Result<Book>.Failure(isbnResult.Error!);

        if (pageCount <= 0)
            return Result<Book>.Failure(Errors.Invalid("Page count must be greater than 0"));

        var authorResult = Guard.AgainstStringRange(authorFullname, 2, 100,
            Errors.Invalid("Author fullname must be between 2 and 100 characters"));

        if (!authorResult.IsSuccess) return Result<Book>.Failure(authorResult.Error!);

        var languageResult = Guard.AgainstStringRange(language, 2, 50,
            Errors.Invalid("Language must be between 2 and 50 characters"));

        if (!languageResult.IsSuccess) return Result<Book>.Failure(languageResult.Error!);


        if (price < 0)
            return Result<Book>.Failure(Errors.Invalid("Price cannot be negative"));

        return Result<Book>.Success(new Book(
            title,
            description,
            isbn,
            pageCount,
            authorFullname,
            language,
            price));
    }

    public Result SetCoverImage(string coverImageUrl)
    {
        if (string.IsNullOrWhiteSpace(coverImageUrl))
            return Result.Failure(Errors.Invalid("Cover image URL cannot be empty"));

        CoverImageUrl = coverImageUrl;
        return Result.Success();
    }

    public void SetAuthor(string authorFullName)
    {
        AuthorFullname = authorFullName;
    }


    public Result AddCategory(Category category)
    {
        if (_bookCategories.Any(bc => bc.Category == category))
            return Result.Failure(Errors.Invalid("Category already exists for this book"));

        var bookCategoryResult = BookCategory.Create(Id, this, category);

        if (!bookCategoryResult.IsSuccess)
            return Result.Failure(bookCategoryResult.Error!);

        _bookCategories.Add(bookCategoryResult.Value!);

        return Result.Success();
    }

    public Result RemoveCategory(Category category)
    {
        var bookCategory = _bookCategories.FirstOrDefault(bc => bc.Category == category);

        if (bookCategory == null)
            return Result.Failure(Errors.Invalid("Category not found for this book"));

        _bookCategories.Remove(bookCategory);
        return Result.Success();
    }

    public Result Update(
        string? title = null,
        string? description = null,
        int? pageCount = null,
        string? language = null,
        decimal? price = null)
    {
        if (!string.IsNullOrWhiteSpace(title))
        {
            var titleResult = Guard.AgainstStringRange(title, 1, 200,
                Errors.Invalid("Title must be between 1 and 200 characters"));

            if (!titleResult.IsSuccess) return titleResult;

            Title = title;
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            var descriptionResult = Guard.AgainstStringRange(description, 10, 2000,
                Errors.Invalid("Description must be between 10 and 2000 characters"));

            if (!descriptionResult.IsSuccess) return descriptionResult;

            Description = description;
        }

        if (pageCount.HasValue)
        {
            if (pageCount.Value <= 0)
                return Result.Failure(Errors.Invalid("Page count must be greater than 0"));

            PageCount = pageCount.Value;
        }

        if (!string.IsNullOrWhiteSpace(language))
        {
            var languageResult = Guard.AgainstStringRange(language, 2, 50,
                Errors.Invalid("Language must be between 2 and 50 characters"));

            if (!languageResult.IsSuccess) return languageResult;

            Language = language;
        }

        if (price.HasValue)
        {
            if (price.Value < 0)
                return Result.Failure(Errors.Invalid("Price cannot be negative"));

            Price = price.Value;
        }

        return Result.Success();
    }
}