using EcommerceLearn.Api.Extensions.Auth;
using EcommerceLearn.Api.Middleware;

namespace EcommerceLearn.Api.Extensions.Core;

public static class ApplicationExtensions
{
    public static WebApplication UseApplication(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors();

        app.UseValidationProblemDetails();

        app.UseHttpsRedirection();

        app.UseJwtAuth();

        app.MapControllers();

        app.MapGraphQL("/graphql");

        return app;
    }
}