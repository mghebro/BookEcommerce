using EcommerceLearn.Domain.Entities.Books;
using HotChocolate.Data.Sorting;

namespace EcommerceLearn.Api.Projection.GraphQL.Books;

public class BookSortType : SortInputType<Book>
{
    protected override void Configure(ISortInputTypeDescriptor<Book> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(b => b.Title);
        descriptor.Field(b => b.Price);
        descriptor.Field(b => b.CreatedAt);
    }
}