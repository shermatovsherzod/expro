using System.Threading.Tasks;

namespace Expro.Services.Interfaces
{
    public interface IEmailService 
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}