using EcommerceLearn.Application.Features.Auth.Commands.Register;
using EcommerceLearn.Api.Extensions.GraphQL;
using EcommerceLearn.Application.Behaviors;
using EcommerceLearn.Api.Extensions.Auth;
using EcommerceLearn.Infrastructure;
using EcommerceLearn.Api.Filters;
using EcommerceLearn.Application;
using FluentValidation;
using MediatR;
using Microsoft.OpenApi.Models;

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
        services.AddSwagger();
        services.AddServerCors();

        return services;
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        return services;
    }

    private static IServiceCollection AddServerCors(this IServiceCollection services)
    {
        services.AddCors(cors =>
        {
            cors.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins("http://localhost:4200") 
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials(); 
            });
        });

        return services;
    }
}