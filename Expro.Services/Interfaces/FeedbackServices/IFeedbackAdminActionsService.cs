using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IFeedbackAdminActionsService
    {
        void Approve(Feedback entity, string userID);
        void Reject(Feedback entity, string userID);
        void Delete(Feedback entity);
    }
}