using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IWithdrawRequestService : IBaseAuthorableService<WithdrawRequest>
    {
        void Add(WithdrawRequest entity, ApplicationUser user);
        IQueryable<WithdrawRequest> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum curUserType,
            UserTypesEnum? filteredUserType,
            int? statusID,
            string authorID);
        bool UserHasNotEnoughtMoneyForWithdrawal(int balance);
        bool AmountIsLessThanMinimum(int amount);
        int GetMinimalAmountInBalanceForWithdrawal();
        bool IsCompleted(WithdrawRequest model);
        void MarkAsCompleted(WithdrawRequest model, string adminID);
        //Document GetFreeByID(int id);
        //Document GetPaidByID(int id);
        //bool EditingIsAllowed(Document entity);
        //bool BelongsToUser(Document entity, string userID);
        //bool AttachedFileIsAllowedToBeDeleted(Document entity);
        //void SubmitForApproval(Document entity, string userID);
        //IQueryable<Document> GetAllForAdmin();
        //IQueryable<Document> GetAllApproved();
        //Document GetApprovedByID(int id);
        //IQueryable<Document> GetAllByCreator(string userID);
        //bool IsFree(Document model);
        //IQueryable<Document> GetAllPurchasedByUser(string purchasedUserID);
    }
}