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

        Document GetSampleDocumentByID(int id);
        bool ApprovingIsAllowed(Document entity);
        void Approve(Document entity, string userID);
        bool RejectingIsAllowed(Document entity);
        void Reject(Document entity, string userID);
        IQueryable<Document> GetSampleDocumentsForAdmin();
        IQueryable<Document> GetSampleDocumentsApproved();
        Document GetSampleDocumentApprovedByID(int id);
    }
}