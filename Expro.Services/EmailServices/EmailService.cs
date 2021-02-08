using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Expro.Services.Interfaces;
using MailKit.Security;

namespace Expro.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта Expro.Uz", "farkhodexpro@yandex.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("farkhodexpro@yandex.com", "5ax7?wuWjm");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}