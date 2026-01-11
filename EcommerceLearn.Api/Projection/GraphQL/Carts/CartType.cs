using EcommerceLearn.Api.Projection.GraphQL.Users;
using EcommerceLearn.Domain.Entities.Carts;

namespace EcommerceLearn.Api.Projection.GraphQL.Carts;

public class CartType : ObjectType<Cart>
{
    protected override void Configure(IObjectTypeDescriptor<Cart> descriptor)
    {
        descriptor.Field(c => c.Id)
            .Type<NonNullType<IdType>>();

        // descriptor.Field(c => c.CartItems)
        //     .Type<NonNullType<ListType<NonNullType<CartItemType>>>>();

        descriptor.Field("totalItems")
            .Type<NonNullType<IntType>>()
            .Resolve(context => context.Parent<Cart>().CartItems.Sum(ci => ci.Quantity));

        descriptor.Field("totalPrice")
            .Type<NonNullType<DecimalType>>()
            .Resolve(context => context.Parent<Cart>().CartItems.Sum(ci => ci.Book.Price * ci.Quantity));

        descriptor.Field(c => c.User)
            .Type<UserType>()
            .Ignore();
    }
}