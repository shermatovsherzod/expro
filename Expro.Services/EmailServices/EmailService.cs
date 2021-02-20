﻿using MimeKit;
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
                await client.ConnectAsync(AppConfiguration.ExproEmailSmtpClient, AppConfiguration.ExproEmailSmtpPort, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(AppConfiguration.ExproEmailAddress, AppConfiguration.ExproEmailPassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async Task SendEmailAsync(
            List<Tuple<string, string>> emails, 
            string subjectUz, string subjectRu,
            string messageUz, string messageRu)
        {
            foreach (var email in emails)
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Администрация сайта Expro.Uz", AppConfiguration.ExproEmailAddress));
                emailMessage.To.Add(new MailboxAddress("", email.Item1));
                emailMessage.Subject = subjectUz + " | " + subjectRu;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = @"" + Appeal(email.Item2, "uz") + @"

" + messageUz + @"

" + Footer("uz") + @"

" + Appeal(email.Item2, "ru") + @"

" + messageRu + @"

" + Footer("ru")
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(AppConfiguration.ExproEmailSmtpClient, AppConfiguration.ExproEmailSmtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(AppConfiguration.ExproEmailAddress, AppConfiguration.ExproEmailPassword);
                    //await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
        }

        private string Appeal(string fullName, string lang = "uz")
        {
            string result;
            if (lang == "uz")
                result = "Хурматли ";
            else
                result = "Уважаемый(ая), ";

            return result + fullName;
        }

        private string Footer(string lang = "uz")
        {
            string result;
            if (lang == "uz")
                result = @"Бу автоматик равишда жунатилинган хат. Илтимос, жавоб ёзманг.
Expro жамоаси.";
            else
                result = @"Это автоматически отправленное письмо. Пожалуйста, не отвечайте на него.
Команда Expro.";

            return result;
        }
    }
}