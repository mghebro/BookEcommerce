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


        descriptor.Field(x => x.Author).Type<UserType>(); // Assuming UserType is defined

        descriptor.Field(x => x.Publisher).Type<UserType>(); // Assuming UserType is defined


        //  descriptor.Field(x => x.BookCategories)
        //     .Type<ListType<BookCategoryType>>(); 


        descriptor.Ignore(x => x.AuthorId);
        descriptor.Ignore(x => x.PublisherId);

        descriptor.Ignore(x => x.BookCategories);
        descriptor.Ignore(b => b.Update(
            default, default, default, default, default
        ));

        descriptor.Ignore(b => b.AddCategory(default));
        descriptor.Ignore(b => b.RemoveCategory(default));
        descriptor.Ignore(b => b.SetCoverImage(default!));
    }
}