using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IFeedbackService : IBaseAuthorableService<Feedback>
    {
        IQueryable<Feedback> GetFeedbackByCreatorID(string userID);
        IQueryable<Feedback> GetAllForAdmin();
        IQueryable<Feedback> GetAllApproved();
        bool FeedbackBelongsToUser(Feedback model, string userID);
        bool FeedbackBelongsToUser(int modelID, string userID);
    }
}