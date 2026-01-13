using EcommerceLearn.Api.Projection.GraphQL.Books;
using EcommerceLearn.Domain.Entities.Carts;

namespace EcommerceLearn.Api.Projection.GraphQL.Carts;

public class CartItemType : ObjectType<CartItem>
{
    protected override void Configure(IObjectTypeDescriptor<CartItem> descriptor)
    {
        descriptor.Field(ci => ci.Id)
            .Type<NonNullType<IdType>>();

        descriptor.Field(ci => ci.BookId)
            .Type<NonNullType<IntType>>();

        descriptor.Field(ci => ci.Book)
            .Type<NonNullType<BookType>>();

        descriptor.Field(ci => ci.Quantity)
            .Type<NonNullType<IntType>>();

        descriptor.Field("subtotal")
            .Type<NonNullType<DecimalType>>()
            .Resolve(context =>
            {
                var cartItem = context.Parent<CartItem>();
                return cartItem.Book.Price * cartItem.Quantity;
            });
    }
}