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
            .Field(e => e.Email)
            .Type<NonNullType<StringType>>()
            .Resolve(e => e.Parent<User>().Email.Value);
        descriptor.Field(x => x.CreatedAt).Type<DateTimeType>();
        
        descriptor.Ignore(x => x.Password);
    }
}