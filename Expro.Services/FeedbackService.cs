using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Expro.Services
{
    public class FeedbackService : BaseAuthorableService<Feedback>, IFeedbackService
    {
        public FeedbackService(IFeedbackRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IQueryable<Feedback> GetFeedbackByCreatorID(string userID)
        {
            return _repository.GetAsIQueryable().Where(c => c.CreatedBy == userID);
        }

        public IQueryable<Feedback> GetAllForAdmin(string feedbackToUser = "")
        {
            if (!String.IsNullOrEmpty(feedbackToUser))
            {
                return _repository.GetAsIQueryable().Where(c => c.ToUser.Id == feedbackToUser);
            }
            return _repository.GetAsIQueryable();
        }

        public IQueryable<Feedback> GetAllApproved(string feedbackToUser = "")
        {
            if (!String.IsNullOrEmpty(feedbackToUser))
            {
                return _repository.GetAsIQueryable().Where(c => c.FeedbackStatusID == (int)FeedbackStatusEnum.Approved && c.ToUser.Id == feedbackToUser);
            }
            return _repository.GetAsIQueryable().Where(c => c.FeedbackStatusID == (int)FeedbackStatusEnum.Approved);
        }

        public bool FeedbackBelongsToUser(Feedback model, string userID)
        {
            if (model == null)
            {
                throw new System.Exception("Отзыв не найден");
            }

            if (model.CreatedBy == userID)
            {
                return true;
            }
            return false;
        }

        public bool FeedbackBelongsToUser(int modelID, string userID)
        {
            var model = _repository.GetByID(modelID);

            if (model == null)
            {
                throw new System.Exception("Отзыв не найден");
            }

            if (model.CreatedBy == userID)
            {
                return true;
            }
            return false;
        }

        public bool FeedbackExist(string toUserID, string createdUserID)
        {
            var model = _repository.GetAsIQueryable().FirstOrDefault(c => c.ToUser.Id == toUserID && c.CreatedBy == createdUserID);

            if (model != null)
            {
                return true;
            }

            return false;
        }

        public int GetRatingStarsCountByExpert(string userID, int starsValue)
        {
            return _repository.GetAsIQueryable().Count(c => c.ToUser.Id == userID && c.Stars == starsValue);
        }

        public double GetOverallRatingByExpert(string userID)
        {
            int sum = _repository.GetAsIQueryable().Where(c => c.ToUser.Id == userID).Sum(c => c.Stars);
            if (sum != 0)
            {
                int k = 0;
                for (int i = 1; i <= 5; i++)
                {
                    k += i * GetRatingStarsCountByExpert(userID, i);
                }

                return k / sum;
            }
            return 0;
            // (5 * 252 + 4 * 124 + 3 * 40 + 2 * 29 + 1 * 33) / 478 = 4.11
        }
    }
}