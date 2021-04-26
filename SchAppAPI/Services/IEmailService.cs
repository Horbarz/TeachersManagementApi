using System.Threading.Tasks;
using SchAppAPI.DOA;

namespace SchAppAPI.Services
{
    public interface IEmailService
    {
        Task SendEmail(MailRequest userEmailOptions);
    }
}