using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Enums.Books;

public sealed class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Field(x => x.Id).Type<NonNullType<IdType>>();
        descriptor.Field(x => x.Title).Type<NonNullType<StringType>>();
        descriptor.Field(x => x.Description).Type<StringType>();
        descriptor.Field(x => x.Isbn).Type<StringType>();
        descriptor.Field(x => x.PageCount).Type<IntType>();
        descriptor.Field(x => x.CoverImageUrl).Type<StringType>();
        descriptor.Field(x => x.AuthorFullname).Type<StringType>();
        descriptor.Field(x => x.Language).Type<StringType>();
        descriptor.Field(x => x.Price).Type<NonNullType<DecimalType>>();
        descriptor.Ignore(x => x.IsDeleted);
        descriptor.Field(x => x.IsAvailable).Type<BooleanType>();
        descriptor.Field(x => x.BookCategory)
            .Type<EnumType<Category>>();
    }
}