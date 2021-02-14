using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IDocumentService : IBaseAuthorableService<Document>
    {
        int PointsForDocumentFree { get; set; }
        int PointsForDocumentPaid { get; set; }
        int PointsForDocumentFreeView { get; set; }
        int PointsForDocumentPurchase { get; set; }
        int PointsForDocumentFreeLike { get; set; }
        int PointsForDocumentPaidLike { get; set; }

        Document GetFreeByID(int id);
        Document GetPaidByID(int id);
        bool EditingIsAllowed(Document entity);
        bool BelongsToUser(Document entity, string userID);
        bool AttachedFileIsAllowedToBeDeleted(Document entity);
        void SubmitForApproval(Document entity, string userID);
        IQueryable<Document> GetAllForAdmin();
        IQueryable<Document> GetAllApproved();
        Document GetApprovedByID(int id);
        //Document GetApprovedWithAnswersAndCommentsByID(int id);
        IQueryable<Document> GetAllByCreator(string userID);
        bool IsFree(Document model);
        IQueryable<Document> GetAllPurchasedByUser(string purchasedUserID);
        DateTime RoundToUp(DateTime inputDateTime);
        IQueryable<Document> GetAllApprovedByUserAndPeriod(
            string userID,
            DateTime startDate, DateTime endDate);

        IQueryable<Document> GetRandomDocuments(int count);
    }
}