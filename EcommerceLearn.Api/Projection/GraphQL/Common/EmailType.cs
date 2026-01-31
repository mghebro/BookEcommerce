using EcommerceLearn.Domain.ValueObjects;
using HotChocolate.Types;

namespace EcommerceLearn.Api.Projection.GraphQL.Common;

public class EmailType : ObjectType<Email>
{
    protected override void Configure(IObjectTypeDescriptor<Email> descriptor)
    {
        descriptor.Name("Email");

        descriptor
            .Field(e => e.Value)
            .Type<NonNullType<StringType>>()
            .Name("value");
    }
}