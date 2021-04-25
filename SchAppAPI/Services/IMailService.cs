using System.Threading.Tasks;
using SchAppAPI.DOA;

namespace SchAppAPI.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
