using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IFeedbackService : IBaseAuthorableService<Feedback>
    {
        IQueryable<Feedback> GetFeedbackByCreatorID(string userID);
        IQueryable<Feedback> GetAllForAdmin(string feedbackToUser = "");
        IQueryable<Feedback> GetAllApproved(string feedbackToUser = "");
        bool FeedbackBelongsToUser(Feedback model, string userID);
        bool FeedbackBelongsToUser(int modelID, string userID);
        bool FeedbackExist(string toUserID, string createdUserID);
        int GetRatingStarsCountByExpert(string userID, int starsValue);
        double GetOverallRatingByExpert(string userID);
        int GetAllStarsCountByExpert(string userID);
    }
}