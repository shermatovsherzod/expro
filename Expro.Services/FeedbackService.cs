using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
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

        public IQueryable<Feedback> GetAllForAdmin()
        {
            return _repository.GetAsIQueryable();
        }

        public IQueryable<Feedback> GetAllApproved()
        {
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
    }
}