﻿using Expro.Common;
using Expro.Common.Utilities;
using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Expro.Services
{
    public class DocumentService : BaseAuthorableService<Document>, IDocumentService
    {
        private IDocumentRepository _documentRepository;

        public DocumentTypesEnum _documentType;

        public int PointsForDocumentFree { get; set; }
        public int PointsForDocumentPaid { get; set; }
        public int PointsForDocumentFreeView { get; set; }
        public int PointsForDocumentPurchase { get; set; }
        public int PointsForDocumentFreeLike { get; set; }
        public int PointsForDocumentPaidLike { get; set; }

        protected AppConfiguration AppConfiguration { get; set; }

        public DocumentService(
            IDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IOptionsSnapshot<AppConfiguration> settings = null)
            : base(repository, unitOfWork)
        {
            _documentRepository = repository;

            if (settings != null)
                AppConfiguration = settings.Value;
        }

        public override void Add(Document entity, string creatorID)
        {
            entity.DocumentTypeID = (int)_documentType;
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Pending;
            //entity.ContentType = DocumentContentTypesEnum.text;

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

        public virtual void SubmitForApproval(Document entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.WaitingForApproval;
            entity.DateSubmittedForApproval = DateTime.Now;

            entity.RejectionDeadline = DateTimeUtils.RoundToUp(entity.DateSubmittedForApproval.Value
                .AddMinutes(AppConfiguration.DocumentRejectionDeadlinePeriodInMinutes));

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
        }

        public IQueryable<Document> GetAllApprovedByUserAndPeriod(
            string userID,
            DateTime startDate, DateTime endDate)
        {
            return base.GetAsIQueryable()
                .Where(m => m.DocumentStatusID == (int)DocumentStatusesEnum.Approved
                    && m.CreatedBy == userID
                    && m.DateCreated >= startDate
                    && m.DateCreated < endDate);
        }

        public IQueryable<Document> GetRandomDocuments(int count)
        {
            return GetAllApproved().OrderByDescending(m => m.ID).Take(count);
            //List<int> allApprovedIDs = GetAllApproved().Select(m => m.ID).ToList();

            //List<int> randomlySelectedIDs = RandomUtils.ExtractRandomInts(allApprovedIDs, count);

            //return GetAllApproved().Where(m => randomlySelectedIDs.Contains(m.ID));
        }
    }
}