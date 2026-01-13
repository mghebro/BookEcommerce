using EcommerceLearn.Api.Projection.GraphQL.Users;
using EcommerceLearn.Domain.Entities.Books;

namespace EcommerceLearn.Api.Projection.GraphQL.Books;

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Field(x => x.Id).Type<IdType>();
        descriptor.Field(x => x.Title).Type<StringType>();
        descriptor.Field(x => x.Description).Type<StringType>();
        descriptor.Field(x => x.Isbn).Type<StringType>();
        descriptor.Field(x => x.PageCount).Type<IntType>();
        descriptor.Field(x => x.CoverImageUrl).Type<StringType>();
        descriptor.Field(x => x.AuthorFullname).Type<StringType>();
        descriptor.Field(x => x.Language).Type<StringType>();
        descriptor.Field(x => x.Price).Type<DecimalType>();

        descriptor.Ignore(x => x.IsDeleted);
        // descriptor.Field(x => x.BookCategories)
        //    .Type<ListType<BookCategoryType>>(); 


        descriptor.Ignore(x => x.BookCategories);
    }
}