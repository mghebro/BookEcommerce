using EcommerceLearn.Application.Features.Auth.Commands.Register;
using EcommerceLearn.Api.Extensions.GraphQL;
using EcommerceLearn.Application.Behaviors;
using EcommerceLearn.Api.Extensions.Auth;
using EcommerceLearn.Infrastructure;
using EcommerceLearn.Api.Filters;
using EcommerceLearn.Application;
using FluentValidation;
using MediatR;

namespace EcommerceLearn.Api.Extensions.Core;

public static class ServerExtensions
{
    public static IServiceCollection AddServer(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddControllers(o => o.Filters.Add<ResultFilter>());
        services.AddValidatorsFromAssembly(typeof(RegisterCommand).Assembly);
        services.AddInfrastructure(config);
        services.AddGraphqlConfiguration();
        services.AddApplication();
        services.AddEndpointsApiExplorer();
        services.AddJwtAuth(config);
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddServerCors();

        return services;
    }

    private static IServiceCollection AddServerCors(this IServiceCollection services)
    {
        services.AddCors(cors =>
        {
            cors.AddDefaultPolicy(policy => { policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); });
        });

        return services;
    }
}