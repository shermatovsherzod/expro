using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IDocumentAdminActionsService : IBaseApprovableByAdminService<Document>
    {
        //bool ApprovingIsAllowed(Document entity);
        //void Approve(Document entity, string userID);
        //bool RejectingIsAllowed(Document entity);
        //void Reject(Document entity, string userID);
        //void RejectionDeadlineReaches(Document document);
    }
}