using EcommerceLearn.Application.Interfaces.SMTP;
using RazorLight;

namespace EcommerceLearn.Infrastructure.SMTP;

public sealed class EmailTemplatesService : IEmailTemplatesService
{
    private readonly RazorLightEngine _razorEngine;

    public EmailTemplatesService()
    {
        _razorEngine = new RazorLightEngineBuilder()
            .UseFileSystemProject(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Emails"))
            .UseMemoryCachingProvider()
            .Build();
    }

    public async Task<string> GenerateEmailAsync<T>(string templateName, T model)
    {
        var template = await _razorEngine.CompileRenderAsync(templateName, model);

        return template;
    }
}