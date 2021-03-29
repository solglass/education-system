using EducationSystem.Core.Config;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Business
{
    public class EmailService
    {
        private SmtpSettings _smtpSettings;
        public EmailService(IOptions<SmtpSettings> options)
        {
            _smtpSettings = options.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", _smtpSettings.From));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpSettings.SmtpServer, _smtpSettings.SmtpPort, true);
                await client.AuthenticateAsync(_smtpSettings.Login, _smtpSettings.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
