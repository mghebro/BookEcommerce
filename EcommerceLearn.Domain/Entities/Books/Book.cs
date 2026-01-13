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
    public bool IsDeleted { get; private set; } = false;
    public bool IsAvailable => !IsDeleted;
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
        string coverImageUrl,
        string authorFullname,
        string language,
        decimal price)
    {
        Title = title;
        Description = description;
        Isbn = isbn;
        PageCount = pageCount;
        CoverImageUrl = coverImageUrl;
        AuthorFullname = authorFullname;
        Language = language;
        Price = price;
    }

    public static Result<Book> Create(
        string title,
        string description,
        string isbn,
        int pageCount,
        string coverImageUrl,
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

        var coverImageUrlResult =
            Guard.AgainstNullOrEmpty(coverImageUrl, Errors.Invalid("Cover image url cannot be null or empty"));

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
            coverImageUrl,
            authorFullname,
            language,
            price));
    }


    public void SetAuthor(string authorFullName)
    {
        AuthorFullname = authorFullName;
    }


    public void AddCategory(Category category)
    {
        if (!_bookCategories.Any(bc => bc.Category == category))
        {
            var bookCategoryResult = BookCategory.Create(Id, this, category);

            if (bookCategoryResult.IsSuccess)
                _bookCategories.Add(bookCategoryResult.Value!);
        }
    }

    public void RemoveCategory(Category category)
    {
        var bookCategory = _bookCategories.FirstOrDefault(bc => bc.Category == category);
        if (bookCategory != null)
            _bookCategories.Remove(bookCategory);
    }

    public void Update(
        string? title = null,
        string? description = null,
        int? pageCount = null,
        string? coverImageUrl = null,
        string? language = null,
        decimal? price = null)
    {
        if (!string.IsNullOrWhiteSpace(title)) Title = title;

        if (!string.IsNullOrWhiteSpace(description)) Description = description;

        if (pageCount.HasValue && pageCount.Value > 0) PageCount = pageCount.Value;
        if (!string.IsNullOrWhiteSpace(coverImageUrl)) CoverImageUrl = coverImageUrl;
        if (!string.IsNullOrWhiteSpace(language)) Language = language;

        if (price.HasValue && price.Value >= 0) Price = price.Value;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
}