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

            Update(entity, userID);
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
    }
}