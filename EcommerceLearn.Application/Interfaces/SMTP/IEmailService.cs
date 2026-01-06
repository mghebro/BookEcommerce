namespace EcommerceLearn.Application.Interfaces.SMTP;

public interface IEmailService
{
    Task SendEmail(string subject, string body, string email);
}