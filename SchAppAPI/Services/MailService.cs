using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SchAppAPI.DOA;
using SchAppAPI.Settings;

namespace SchAppAPI.Services
{
    public class MailService : IMailService
    {

        private readonly MailSettings _mailSettings;
        public async Task SendEmailAsync(MailRequest mailRequest)
        {

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("horbarz007@gmail.com");
            //email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect("webmail.selahseries.com", 25, SecureSocketOptions.None);
            smtp.Authenticate("connect@selahseries.com", "ybD3r?26");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}