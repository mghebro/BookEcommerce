using HotChocolate.Data.Filters;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Enums.Books;

public class BookFilterType : FilterInputType<Book>
{
    protected override void Configure(IFilterInputTypeDescriptor<Book> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(b => b.Title);
        descriptor.Field(b => b.AuthorFullname);
        descriptor.Field(b => b.Language);
        descriptor.Field(b => b.Price);
        descriptor.Field(b => b.IsAvailable);

        descriptor.Field(x => x.BookCategory);

    }
}