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
    public class DocumentService : BaseAuthorableService<Document>, IDocumentService
    {
        private int tmpPeriodMinutes = 1;

        public DocumentService(IDocumentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public void AddSampleDocument(Document entity, string creatorID)
        {
            entity.DocumentTypeID = (int)DocumentTypesEnum.SampleDocument;
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Pending;
            entity.ContentType = DocumentContentTypesEnum.text;

            Add(entity, creatorID);
        }

        bool StatusIsPending(Document entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.Pending;
        }

        bool StatusIsWaitingForApproval(Document entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.WaitingForApproval;
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
            entity.RejectionDeadline = entity.DateSubmittedForApproval.Value.AddMinutes(tmpPeriodMinutes);
#else
            entity.CancellationDeadline = RoundToUp(entity.DateSubmittedForApproval.Value.AddMinutes(7 200)); //5 days
#endif
            Update(entity, userID);
        }

        public IQueryable<Document> GetSampleDocuments()
        {
            return GetAsIQueryable()
                .Where(m => m.DocumentTypeID == (int)DocumentTypesEnum.SampleDocument);
        }

        public Document GetSampleDocumentByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (model.DocumentTypeID != (int)DocumentTypesEnum.SampleDocument)
                    model = null;
            }

            return model;
        }

        public bool ApprovingIsAllowed(Document entity)
        {
            return StatusIsWaitingForApproval(entity);
        }

        public void Approve(Document entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Approved;
            entity.DateApproved = DateTime.Now;

            Update(entity, userID);
        }

        public bool RejectingIsAllowed(Document entity)
        {
            return StatusIsWaitingForApproval(entity);
        }

        public void Reject(Document entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Rejected;
            entity.DateRejected = DateTime.Now;

            Update(entity, userID);
        }

        //public IQueryable<Document> 

        public IQueryable<Document> GetSampleDocumentsForAdmin()
        {
            return GetAsIQueryable()
                .Where(m => m.DocumentStatusID != (int)DocumentStatusesEnum.Pending);
        }

        public IQueryable<Document> GetSampleDocumentsApproved()
        {
            return GetAsIQueryable()
                .Where(m => m.DocumentStatusID == (int)DocumentStatusesEnum.Approved);
        }

        public Document GetSampleDocumentApprovedByID(int id)
        {
            var model = GetSampleDocumentByID(id);
            if (model != null)
            {
                if (model.DocumentStatusID != (int)DocumentStatusesEnum.Approved)
                    model = null;
            }

            return model;
        }

        public IQueryable<Document> GetSampleDocumentsForExpert(string userID)
        {
            return GetSampleDocuments()
                .Where(m => m.CreatedBy == userID);
        }

        public void IncrementNumberOfViews(Document model)
        {
            try
            {
                if (model == null)
                    return;

                int number = model.NumberOfViews;
                number++;
                model.NumberOfViews = number;

                Update(model);
            }
            catch
            { }
        }

        public void IncrementNumberOfPurchases(Document model)
        {
            try
            {
                if (model == null)
                    return;

                int number = model.NumberOfPurchases;
                number++;
                model.NumberOfPurchases = number;

                Update(model);
            }
            catch
            { }
        }

        public bool IsFree(Document model)
        {
            if (model == null)
                throw new Exception("Document is null");

            return model.PriceType == DocumentPriceTypesEnum.Free;
        }

        public IQueryable<Document> GetDocumentsPurchasedByUser(ApplicationUser user)
        {
            return user.DocumentsPurchased.Select(m => m.Document).AsQueryable();
        }

        public void RejectionDeadlineReaches(Document model)
        {
            if (!RejectingIsAllowed(model))
                return;

            Reject(model, "634a8718-167d-4b77-98bb-7548340e95b2"); //add botUser
        }

        private DateTime RoundToUp(DateTime inputDateTime)
        {
            return inputDateTime.Date.AddDays(1).AddSeconds(-1);
        }

        public IQueryable<Document> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum curUserType,
            int? statusID,
            DocumentPriceTypesEnum? priceType,
            string authorID,
            ApplicationUser purchaser)
        {
            recordsTotal = 0;
            recordsFiltered = 0;
            error = "";

            try
            {
                IQueryable<Document> documents;

                if (curUserType == UserTypesEnum.Admin)
                    documents = GetSampleDocumentsForAdmin();
                else if (curUserType == UserTypesEnum.Expert)
                    documents = GetSampleDocumentsForExpert(authorID);
                else if (curUserType == UserTypesEnum.SimpleUser)
                    documents = GetDocumentsPurchasedByUser(purchaser);
                else
                    documents = GetSampleDocumentsApproved();

                recordsTotal = documents.Count();

                if (statusID.HasValue)
                    documents = documents.Where(m => m.DocumentStatusID == statusID.Value);
                if (priceType.HasValue)
                    documents = documents.Where(m => m.PriceType == priceType.Value);



                recordsFiltered = documents.Count();

                documents = documents.OrderByDescending(m => m.DateModified);
                if (start.HasValue && start.Value > 0)
                    documents = documents.Skip(start.Value);
                if (length.HasValue && length.Value > 0)
                    documents = documents.Take(length.Value);

                //IQueryable<VideoRequest> data = videoRequests.Skip(start).Take(length.Value);

                //return documents.OrderByDescending(m => m.DateModified);
                return documents;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                if (ex.InnerException != null)
                    error += ". Inner exception: " + ex.InnerException.Message;

                return Enumerable.Empty<Document>().AsQueryable();
            }
        }
    }
}