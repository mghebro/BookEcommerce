using EcommerceLearn.Application.Features.Auth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceLearn.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthEmailService, AuthEmailService>();

        return services;
    }
}