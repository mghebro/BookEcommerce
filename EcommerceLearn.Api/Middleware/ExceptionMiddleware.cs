using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace EcommerceLearn.Api.Middleware;

public sealed class ExceptionMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext ctx)
    {
        try
        {
            await next(ctx);
        }
        catch (ValidationException ex)
        {
            ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
            ctx.Response.ContentType = "application/problem+json";

            var errors = ex.Errors
                .GroupBy(e => string.IsNullOrWhiteSpace(e.PropertyName) ? "General" : e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var details = new ValidationProblemDetails
            {
                Errors = errors,
                Detail = ex.Message,
                Title = "Validation Error",
                Status = StatusCodes.Status400BadRequest
            };

            await ctx.Response.WriteAsJsonAsync(details);
        }
    }
}

public static class ExceptionExtensions
{
    public static IApplicationBuilder UseValidationProblemDetails(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}