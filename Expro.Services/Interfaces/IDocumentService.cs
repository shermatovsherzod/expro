using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IDocumentService : IBaseAuthorableService<Document>
    {
        void AddSampleDocument(Document entity, string creatorID);
        bool EditingIsAllowed(Document entity);
        bool BelongsToUser(Document entity, string userID);
        bool AttachedFileIsAllowedToBeDeleted(Document entity);
        void SubmitForApproval(Document entity, string userID);
    }
}