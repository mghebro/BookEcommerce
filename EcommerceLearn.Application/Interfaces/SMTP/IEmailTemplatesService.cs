namespace EcommerceLearn.Application.Interfaces.SMTP;

public interface IEmailTemplatesService
{
    Task<string> GenerateEmailAsync<T>(string templateName, T model);
}