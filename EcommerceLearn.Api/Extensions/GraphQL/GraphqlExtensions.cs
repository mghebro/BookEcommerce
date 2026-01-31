using EcommerceLearn.Api.Projection.GraphQL.Books;
using EcommerceLearn.Api.Projection.GraphQL.Carts;
using EcommerceLearn.Api.Projection.GraphQL.Common;
using EcommerceLearn.Api.Projection.GraphQL.Orders;
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
            .ModifyCostOptions(opt => { opt.EnforceCostLimits = false; })
            // .AddMutationType()
            .AddProjections()
            .AddFiltering()
            .AddQueryType()
            .AddSorting()
            // Queries
            .AddTypeExtension<UserQueries>()
            .AddTypeExtension<BookQueries>()
            .AddTypeExtension<CartQueries>()
            .AddTypeExtension<OrderQueries>()
            // Types
            .AddType<UserType>()
            .AddType<BookType>()
            .AddType<CartType>()
            .AddType<CartItemType>()
            .AddType<OrderItemType>()
            .AddType<OrderType>()
            .AddType<EmailType>()
            .AddType<BookFilterType>()
            .AddType<BookSortType>()
            .AddType<UserAddressType>();

        return services;
    }
}