using EcommerceLearn.Api.Projection.GraphQL.Books;
using EcommerceLearn.Api.Projection.GraphQL.Carts;
using EcommerceLearn.Api.Projection.GraphQL.Users;

namespace EcommerceLearn.Api.Extensions.GraphQL;

public static class GraphqlExtensions
{
    public static IServiceCollection AddGraphqlConfiguration(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
            .ModifyPagingOptions(opt => opt.IncludeTotalCount = true)
            .AddPagingArguments()
            .AddAuthorization()
            // .AddMutationType()
            .AddProjections()
            .AddFiltering()
            .AddQueryType()
            .AddSorting()
            // Queries
            .AddTypeExtension<UserQueries>()
            .AddTypeExtension<BookQueries>()
            .AddTypeExtension<CartQueries>()
            // Types
            .AddType<UserType>()
            .AddType<BookType>()
            .AddType<CartType>();

        return services;
    }
}