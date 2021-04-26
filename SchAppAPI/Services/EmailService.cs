using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SchAppAPI.DOA;
using SchAppAPI.Models;

namespace SchAppAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPConfigModel _smtpConfigModel;

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {

            _smtpConfigModel = smtpConfig.Value;
        }

        public async Task SendEmail(MailRequest userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfigModel.SenderAddress, _smtpConfigModel.SenderDisplayName),
                IsBodyHtml = _smtpConfigModel.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.ToEmail)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfigModel.UserName, _smtpConfigModel.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfigModel.Host,
                Port = _smtpConfigModel.Port,
                UseDefaultCredentials = _smtpConfigModel.UseDefaultCredentials,
                EnableSsl = _smtpConfigModel.EnableSSL,
                Credentials = networkCredential

            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);

        }
    }
}