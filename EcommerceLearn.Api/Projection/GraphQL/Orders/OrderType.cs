using EcommerceLearn.Api.Projection.GraphQL.Common;
using EcommerceLearn.Api.Projection.GraphQL.Users;
using EcommerceLearn.Domain.Entities.Orders;
using HotChocolate.Types;

namespace EcommerceLearn.Api.Projection.GraphQL.Orders;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        descriptor.Field(o => o.Id);
        descriptor.Field(o => o.UserId);
        descriptor.Field(o => o.TotalAmount);
        descriptor.Field(o => o.Status);

        descriptor.Field(x => x.ShippingAddress)
            .Type<ListType<NonNullType<UserAddressType>>>();

        descriptor.Field(o => o.OrderItems)
            .Type<NonNullType<ListType<NonNullType<OrderItemType>>>>();
    }
}