using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Application.Interfaces.Security;
using EcommerceLearn.Application.Interfaces.SMTP;
using EcommerceLearn.Infrastructure.Persistence;
using EcommerceLearn.Infrastructure.Security;
using EcommerceLearn.Infrastructure.SMTP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceLearn.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IDataContext>(sp => sp.GetRequiredService<DataContext>());

        var jwt = config.GetSection("Jwt").Get<JwtSettings>()!;
        services.AddSingleton(jwt);
        services.AddScoped<IEmailTemplatesService, EmailTemplatesService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IEmailService, EmailService>();
        return services;
    }
}