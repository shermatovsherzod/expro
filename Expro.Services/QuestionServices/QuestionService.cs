using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Expro.Services
{
    public class QuestionService : BaseAuthorableService<Question>, IQuestionService
    {
        private IQuestionRepository _questionRepository;
        protected int _tmpPeriodMinutes = 20;

        public QuestionService(IQuestionRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _questionRepository = repository;
        }

        public override void Add(Question entity, string creatorID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Pending;
            base.Add(entity, creatorID);
        }

        public IQueryable<Question> GetManyWithRelatedDataAsIQueryable()
        {
            return _questionRepository.GetManyWithRelatedDataAsIQueryable();
        }

        public Question GetFreeByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (!IsFree(model))
                    model = null;
            }

            return model;
        }

        public Question GetPaidByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (model.PriceType != DocumentPriceTypesEnum.Paid)
                    model = null;
            }

            return model;
        }

        bool StatusIsPending(Question entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.Pending;
        }

        public bool EditingIsAllowed(Question entity)
        {
            return StatusIsPending(entity);
        }

        public bool BelongsToUser(Question entity, string userID)
        {
            if (entity == null)
                return false;

            return entity.CreatedBy == userID;
        }

        public bool AttachedFileIsAllowedToBeDeleted(Question entity)
        {
            return EditingIsAllowed(entity);
        }

        public void SubmitForApproval(Question entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.WaitingForApproval;
            entity.DateSubmittedForApproval = DateTime.Now;
#if DEBUG
            //tmpPeriodMinutes = 2880;
            entity.RejectionDeadline = entity.DateSubmittedForApproval.Value.AddMinutes(_tmpPeriodMinutes);
#else
            entity.CancellationDeadline = RoundToUp(entity.DateSubmittedForApproval.Value.AddMinutes(7 200)); //5 days
#endif
            Update(entity, userID);
        }

        public IQueryable<Question> GetAllForAdmin()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.DocumentStatusID != (int)DocumentStatusesEnum.Pending);
        }

        public IQueryable<Question> GetAllApproved()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.DocumentStatusID == (int)DocumentStatusesEnum.Approved);
        }

        public Question GetApprovedByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (model.DocumentStatusID != (int)DocumentStatusesEnum.Approved)
                    model = null;
            }

            return model;
        }

        private Question GeWithAnswersAndCommentsByID(int id)
        {
            var model = _questionRepository.GeWithAnswersAndCommentsByID(id);
            return model;
        }

        public Question GetApprovedWithAnswersAndCommentsByID(int id)
        {
            var model = GeWithAnswersAndCommentsByID(id);

            if (model != null)
            {
                if (model.DocumentStatusID != (int)DocumentStatusesEnum.Approved)
                    model = null;
            }

            return model;
        }

        public IQueryable<Question> GetAllByCreator(string userID)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.CreatedBy == userID);
        }

        public bool IsFree(Question model)
        {
            if (model == null)
                throw new Exception("Question is null");

            return model.PriceType == DocumentPriceTypesEnum.Free;
        }

        public IQueryable<Question> GetAllAnsweredByUser(string answeredUserID)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.Answers
                    .Select(n => n.CreatedBy)
                    .Contains(answeredUserID));
        }

        public DateTime RoundToUp(DateTime inputDateTime)
        {
            return inputDateTime.Date.AddDays(1).AddSeconds(-1);
        }

        public void CompleteWithDistribution(Question question, string userID)
        {
            question.QuestionFeeIsDistributed = true;
            Complete(question, userID);
        }

        public void Complete(Question question, string userID)
        {
            question.QuestionIsCompleted = true;
            question.DateQuestionCompleted = DateTime.Now;

            Update(question, userID);
        }

        public bool AdminIsAllowedToComplete(Question question)
        {
            var now = DateTime.Now;

            if (!question.DateApproved.HasValue)
                return false;

            if (question.DateApproved.Value.AddDays(1) < now)
                //if (question.DateApproved.Value.AddMinutes(10) < now)
                return true;

            return false;
        }

        public bool IsCompleted(Question question)
        {
            return question.QuestionIsCompleted.HasValue ?
                question.QuestionIsCompleted.Value : false;
        }
    }
}