using EcommerceLearn.Api.Projection.GraphQL.Common;
using EcommerceLearn.Domain.Entities.Addresses;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Api.Projection.GraphQL.Users;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(x => x.Id).Type<IdType>();
        descriptor.Field(x => x.FirstName).Type<StringType>();
        descriptor.Field(x => x.LastName).Type<StringType>();
        descriptor
            .Field(x => x.Email)
            .Type<NonNullType<EmailType>>();
        descriptor.Field(x => x.CreatedAt).Type<DateTimeType>();

        descriptor.Ignore(x => x.Password);
        descriptor.Field(x => x.UserAddresses).Type<ListType>();
        descriptor.Field(x => x.UserAddresses)
            .Type<ListType<NonNullType<UserAddressType>>>()
            .Resolve(context =>
                context.Parent<User>()
                    .UserAddresses?.Where(a => !a.IsDeleted) ?? Enumerable.Empty<UserAddress>()
            );
    }
}