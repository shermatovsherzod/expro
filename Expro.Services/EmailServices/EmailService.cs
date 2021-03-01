using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Expro.Services.Interfaces;
using MailKit.Security;
using System.Collections.Generic;
using System;
using Expro.Common;
using Microsoft.Extensions.Options;

namespace Expro.Services
{
    public class EmailService : IEmailService
    {
        protected AppConfiguration AppConfiguration { get; set; }

        public EmailService(IOptionsSnapshot<AppConfiguration> settings = null)
        {
            if (settings != null)
                AppConfiguration = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта Expro.Uz", AppConfiguration.ExproEmailAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                //await client.ConnectAsync(AppConfiguration.ExproEmailSmtpClient, AppConfiguration.ExproEmailSmtpPort, SecureSocketOptions.SslOnConnect);
                await client.ConnectAsync(AppConfiguration.ExproEmailSmtpClient, AppConfiguration.ExproEmailSmtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(AppConfiguration.ExproEmailUsername, AppConfiguration.ExproEmailPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        //Mirazam
        public async Task SendAutomaticallyGeneratedEmailAsync(
            List<Tuple<string, string>> emails,
            string subjectUz, string subjectRu,
            string messageUz, string messageRu)
        {
            foreach (var email in emails)
            {
                string emailBox = email.Item1;
                string subject = subjectUz + " | " + subjectRu;
                string bodyText = @"" + Appeal(email.Item2, "uz") + @"

        " + messageUz + @"

        " + Footer("uz") + @"<br>

        " + Appeal(email.Item2, "ru") + @"

        " + messageRu + @"

        " + Footer("ru");

                await SendEmailAsync(emailBox, subject, bodyText);
            }
        }

        private string Appeal(string fullName, string lang = "uz")
        {
            string result = "<p>";
            if (lang == "uz")
                result += "Хурматли ";
            else
                result += "Уважаемый(ая), ";

            return result + fullName + "</p>";
        }

        private string Footer(string lang = "uz")
        {
            string result;
            if (lang == "uz")
                result = @"<p>Бу автоматик равишда жунатилинган хат. Илтимос, жавоб ёзманг.<br>
Expro жамоаси.</p>";
            else
                result = @"<p>Это автоматически отправленное письмо. Пожалуйста, не отвечайте на него.<br>
<p>Команда Expro.</p>";

            return result;
        }
    }
}