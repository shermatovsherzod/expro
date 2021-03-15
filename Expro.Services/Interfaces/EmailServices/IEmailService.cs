using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expro.Services.Interfaces
{
    public interface IEmailService 
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmail2Async(string email, string subject, string message);
        Task SendAutomaticallyGeneratedEmailAsync(
            List<Tuple<string, string>> emails,
            string subjectUz, string subjectRu,
            string messageUz, string messageRu);
    }
}