using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IDocumentService : IBaseAuthorableService<Document>
    {
        bool EditingIsAllowed(Document entity);
        bool BelongsToUser(Document entity, string userID);
        bool AttachedFileIsAllowedToBeDeleted(Document entity);
        void SubmitForApproval(Document entity, string userID);
        IQueryable<Document> GetAllForAdmin();
        IQueryable<Document> GetAllApproved();
        Document GetApprovedByID(int id);
        IQueryable<Document> GetAllByCreator(string userID);
        bool IsFree(Document model);
        IQueryable<Document> GetAllPurchasedByUser(ApplicationUser user);
    }
}