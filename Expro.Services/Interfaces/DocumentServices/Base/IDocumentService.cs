using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IDocumentService : IBaseAuthorableService<Document>
    {
        Document GetFreeByID(int id);
        Document GetPaidByID(int id);
        bool EditingIsAllowed(Document entity);
        bool BelongsToUser(Document entity, string userID);
        bool AttachedFileIsAllowedToBeDeleted(Document entity);
        void SubmitForApproval(Document entity, string userID);
        IQueryable<Document> GetAllForAdmin();
        IQueryable<Document> GetAllApproved();
        Document GetApprovedByID(int id);
        Document GetApprovedWithAnswersAndCommentsByID(int id);
        IQueryable<Document> GetAllByCreator(string userID);
        bool IsFree(Document model);
        IQueryable<Document> GetAllPurchasedByUser(string purchasedUserID);
        DateTime RoundToUp(DateTime inputDateTime);
    }
}