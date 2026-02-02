using EcommerceLearn.Api.Projection.GraphQL.Books;
using EcommerceLearn.Domain.Entities.Orders;

namespace EcommerceLearn.Api.Projection.GraphQL.Orders;

public class OrderItemType : ObjectType<OrderItem>
{
    protected override void Configure(IObjectTypeDescriptor<OrderItem> descriptor)
    {
        descriptor.Field(oi => oi.Id);
        descriptor.Field(oi => oi.BookId);
        descriptor.Field(oi => oi.Quantity);
        descriptor.Field(oi => oi.Price);

        descriptor.Field(oi => oi.Book)
            .Type<NonNullType<BookType>>();
        descriptor.Field("subtotal")
            .Type<NonNullType<DecimalType>>()
            .Resolve(context =>
            {
                var orderItem = context.Parent<OrderItem>();
                return orderItem.Price * orderItem.Quantity;
            });
    }
}