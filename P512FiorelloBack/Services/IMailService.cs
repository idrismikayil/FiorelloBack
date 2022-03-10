using P512FiorelloBack.Models;
using System.Threading.Tasks;

namespace P512FiorelloBack.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
