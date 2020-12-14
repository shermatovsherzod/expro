using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IQuestionAdminActionsService
    {
        bool ApprovingIsAllowed(Question entity);
        void Approve(Question entity, string userID);
        bool RejectingIsAllowed(Question entity);
        void Reject(Question entity, string userID);
        void RejectionDeadlineReaches(Question Question);
        void CompletionDeadlineReaches(Question Question);
    }
}