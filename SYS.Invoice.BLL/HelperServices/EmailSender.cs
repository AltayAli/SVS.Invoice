using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SYS.Invoice.BLL.Infrastructure;
using SYS.Invoice.BLL.Models;
using System.Threading.Tasks;

namespace SYS.Invoice.BLL.HelperServices
{
    public class EmailSender : IEmailSender
    {
        private const int _tlsPort = 587;
        private readonly MailSettings _mailSettings;
        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {

            MimeMessage email = new();
            email.Sender = MailboxAddress.Parse(_mailSettings.SenderMail);
            email.To.Add(MailboxAddress.Parse(_mailSettings.SendMail));
            email.Subject = mailRequest.Subject;
            BodyBuilder builder = new();

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using SmtpClient smtp = new();

            SecureSocketOptions secureSocketOptions = SecureSocketOptions.SslOnConnect;

            if (_mailSettings.Port == _tlsPort)
                secureSocketOptions = SecureSocketOptions.StartTls;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port);
            smtp.Authenticate(_mailSettings.SenderMail, _mailSettings.SenderPassword);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}