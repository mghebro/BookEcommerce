using EcommerceLearn.Application.Interfaces.SMTP;
using System.Net.Mail;
using System.Net;

namespace EcommerceLearn.Infrastructure.SMTP;

public sealed class EmailService : IEmailService
{
    private readonly string _email = "giorgimgebrishvili2008@gmail.com";
    private readonly string _password = "fasz rhsu aiou eese";

    public async Task SendEmail(string subject, string body, string email)
    {
        using var mail = new MailMessage();
        mail.From = new MailAddress(_email, "Book");
        mail.IsBodyHtml = true;
        mail.Subject = subject;
        mail.To.Add(email);
        mail.Body = body;

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(_email, _password)
        };

        await smtpClient.SendMailAsync(mail);
    }
}