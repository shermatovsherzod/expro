using Expro.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Services.Interfaces
{
    public interface IHangfireService
    {
        string CreateJobForDocumentRejectionDeadline(Document document);
        string CreateJobForQuestionRejectionDeadline(Question document);
        string CreateJobForQuestionCompletionDeadline(Question document);

        void CreateRecurringJobForUpdatingRatingsForAllExperts();

        void CancelJob(string jobID);
    }
}
