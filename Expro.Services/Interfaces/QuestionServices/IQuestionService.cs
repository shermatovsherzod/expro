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
        bool EditingIsAllowed(Question entity);
        bool BelongsToUser(Question entity, string userID);
        bool AttachedFileIsAllowedToBeDeleted(Question entity);
        void SubmitForApproval(Question entity, string userID);
        IQueryable<Question> GetAllForAdmin();
        IQueryable<Question> GetAllApproved();
        Question GetApprovedByID(int id);
        Question GetApprovedWithAnswersAndCommentsByID(int id);
        IQueryable<Question> GetAllByCreator(string userID);
        bool IsFree(Question model);
        IQueryable<Question> GetAllAnsweredByUser(string answeredUserID);
        DateTime RoundToUp(DateTime inputDateTime);
        void CompleteWithDistribution(Question question, string userID);
        void Complete(Question question, string userID);
        bool AdminIsAllowedToComplete(Question question);
        bool IsCompleted(Question question);
        IQueryable<Question> GetAllWhereFeeIsDistributedByCreator(string creatorID);
        IQueryable<Question> GetRandomQuestions(int count);
    }
}