using EcommerceLearn.Domain.Entities.Addresses;

namespace EcommerceLearn.Api.Projection.GraphQL.Users;

public class UserAddressType : ObjectType<UserAddress>
{
    protected override void Configure(IObjectTypeDescriptor<UserAddress> descriptor)
    {
        descriptor.Field(a => a.Id).Type<IdType>();
        descriptor.Field(a => a.Country).Type<StringType>();
        descriptor.Field(a => a.City).Type<StringType>();
        descriptor.Field(a => a.Street).Type<StringType>();
        descriptor.Field(a => a.PostalCode).Type<StringType>();
        descriptor.Field(a => a.IsDefault).Type<BooleanType>();

        descriptor.Ignore(a => a.UserId);
        descriptor.Ignore(a => a.IsDeleted);
    }
}