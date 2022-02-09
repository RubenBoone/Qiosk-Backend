using QioskAPI.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System.Net;
using QioskAPI.Interfaces;

namespace QioskAPI.Services
{
    public class MailService : IMailService
    {
        private readonly EmailSender _mail;
        public MailService(IOptions<EmailSender> mail)
        {
            _mail = mail.Value;
        }



        public async void SendEmailAsync(string recipientEmail, string recipientName, string subject, string message)
        {

            var messageS = new MimeMessage();
            messageS.From.Add(new MailboxAddress(_mail.SenderName, _mail.SenderEmail));
            messageS.To.Add(new MailboxAddress(recipientName, recipientEmail));
            messageS.Subject = subject != "" ? subject : "Test @ Qiosk";

            messageS.Body = new TextPart("html")
            {
                Text = message != "" ? message : "Heyya! Test @ Qiosk!"
            };


            var client = new SmtpClient();
            await client.ConnectAsync(_mail.Server, _mail.Port, true);
            await client.AuthenticateAsync(new NetworkCredential(_mail.SenderEmail, _mail.Password));
            await client.SendAsync(messageS);
            await client.DisconnectAsync(true);

        }
    }
}