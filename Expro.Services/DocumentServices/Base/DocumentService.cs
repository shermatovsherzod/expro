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
    public class DocumentService : BaseAuthorableService<Document>, IDocumentService
    {
        private IDocumentRepository _documentRepository;
        protected int _tmpPeriodMinutes = 5;

        public DocumentTypesEnum _documentType;

        public DocumentService(IDocumentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _documentRepository = repository;
        }

        public override void Add(Document entity, string creatorID)
        {
            entity.DocumentTypeID = (int)_documentType;
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Pending;
            entity.ContentType = DocumentContentTypesEnum.text;

            base.Add(entity, creatorID);
        }

        public override IQueryable<Document> GetAsIQueryable()
        {
            return base.GetAsIQueryable()
                .Where(m => m.DocumentTypeID == (int)_documentType);
        }

        public IQueryable<Document> GetManyWithRelatedDataAsIQueryable()
        {
            return _documentRepository.GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.DocumentTypeID == (int)_documentType);
        }

        public override Document GetByID(int id)
        {
            //var model = base.GetByID(id);
            var model = _documentRepository.GeWithRelatedDataByID(id);
            if (model != null)
            {
                if (DocumentTypeIsNotDefined())
                    return model;

                if (model.DocumentTypeID != (int)_documentType)
                    model = null;
            }

            return model;
        }

        public Document GetFreeByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (!IsFree(model))
                    model = null;
            }

            return model;
        }

        public Document GetPaidByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (model.PriceType != DocumentPriceTypesEnum.Paid)
                    model = null;
            }

            return model;
        }

        bool DocumentTypeIsNotDefined()
        {
            return (int)_documentType == 0;
        }

        bool StatusIsPending(Document entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.Pending;
        }

        public bool EditingIsAllowed(Document entity)
        {
            return StatusIsPending(entity);
        }

        public bool BelongsToUser(Document entity, string userID)
        {
            if (entity == null)
                return false;

            return entity.CreatedBy == userID;
        }

        public bool AttachedFileIsAllowedToBeDeleted(Document entity)
        {
            return EditingIsAllowed(entity);
        }

        public void SubmitForApproval(Document entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.WaitingForApproval;
            entity.DateSubmittedForApproval = DateTime.Now;
#if DEBUG
            //tmpPeriodMinutes = 2880;
            entity.RejectionDeadline = RoundToUp(entity.DateSubmittedForApproval.Value.AddMinutes(_tmpPeriodMinutes));
#else
            entity.CancellationDeadline = RoundToUp(entity.DateSubmittedForApproval.Value.AddMinutes(7 200)); //5 days
#endif
            Update(entity, userID);
        }

        public IQueryable<Document> GetAllForAdmin()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.DocumentStatusID != (int)DocumentStatusesEnum.Pending);
        }

        public IQueryable<Document> GetAllApproved()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.DocumentStatusID == (int)DocumentStatusesEnum.Approved);
        }

        public Document GetApprovedByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (model.DocumentStatusID != (int)DocumentStatusesEnum.Approved)
                    model = null;
            }

            return model;
        }

        private Document GeWithAnswersAndCommentsByID(int id)
        {
            var model = _documentRepository.GeWithAnswersAndCommentsByID(id);
            if (model != null)
            {
                if (DocumentTypeIsNotDefined())
                    return model;

                if (model.DocumentTypeID != (int)_documentType)
                    model = null;
            }

            return model;
        }

        public Document GetApprovedWithAnswersAndCommentsByID(int id)
        {
            var model = GeWithAnswersAndCommentsByID(id);

            if (model != null)
            {
                if (model.DocumentStatusID != (int)DocumentStatusesEnum.Approved)
                    model = null;
            }

            return model;
        }

        public IQueryable<Document> GetAllByCreator(string userID)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.CreatedBy == userID);
        }

        public bool IsFree(Document model)
        {
            if (model == null)
                throw new Exception("Document is null");

            return model.PriceType == DocumentPriceTypesEnum.Free;
        }

        public IQueryable<Document> GetAllPurchasedByUser(string purchasedUserID /*ApplicationUser userID*/)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.DocumentTypeID == (int)_documentType
                    && m.UsersPurchasedThisDocument.Select(n => n.UserID).Contains(purchasedUserID));
            //return user.DocumentsPurchased
            //    .Where(m => m.Document.DocumentTypeID == (int)_documentType)
            //    .Select(m => m.Document).AsQueryable();
        }

        private DateTime RoundToUp(DateTime inputDateTime)
        {
            return inputDateTime.Date.AddDays(1).AddSeconds(-1);
        }

        public void CompleteQuestion(Document question, string userID)
        {
            question.QuestionIsCompleted = true;
            question.DateQuestionCompleted = DateTime.Now;

            Update(question, userID);
        }
    }
}