using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IQuestionService : IBaseAuthorableService<Question>
    {
        Question GetFreeByID(int id);
        Question GetPaidByID(int id);
        bool StatusIsApproved(Question entity);
        bool EditingIsAllowed(Question entity);
        bool BelongsToUser(Question entity, string userID);
        bool AttachedFileIsAllowedToBeDeleted(Question entity);
        void Publish(Question entity, string userID);
        void SubmitForApproval(Question entity, string userID);
        IQueryable<Question> GetAllForAdmin();
        IQueryable<Question> GetAllApproved();
        Question GetApprovedByID(int id);
        Question GetApprovedWithAnswersAndCommentsByID(int id);
        IQueryable<Question> GetAllByCreator(string userID);
        bool IsFree(Question model);
        IQueryable<Question> GetAllAnsweredByUser(string answeredUserID);
        void CompleteWithDistribution(Question question, string userID);
        void Complete(Question question, string userID);
        bool AdminIsAllowedToComplete(Question question);
        bool IsCompleted(Question question);
        IQueryable<Question> GetAllWhereFeeIsDistributedByCreator(string creatorID);
        IQueryable<Question> GetRandomQuestions(int count);
        bool SettingAsPaidIsAllowed(Question question);
        void SetAsPaid(Question question, int fee, string userID);
    }
}