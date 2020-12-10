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
    public class DocumentSearchService : IDocumentSearchService//DocumentService, ISampleDocumentService
    {
        protected IDocumentService DocumentService;

        public DocumentSearchService() { }

        public DocumentSearchService(IDocumentService documentService)
        {
            DocumentService = documentService;
        }

        public virtual IQueryable<Document> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,
            int? statusID,
            DocumentPriceTypesEnum? priceType,
            string authorID,
            string purchasedUserID,
            int[] lawAreas)
        {
            recordsTotal = 0;
            recordsFiltered = 0;
            error = "";

            try
            {
                IQueryable<Document> documents;

                if (curUserType.HasValue)
                {
                    if (!string.IsNullOrWhiteSpace(purchasedUserID))
                        documents = DocumentService.GetAllPurchasedByUser(purchasedUserID);
                    else if (curUserType == UserTypesEnum.Admin)
                        documents = DocumentService.GetAllForAdmin();
                    else if (curUserType == UserTypesEnum.Expert)
                        documents = DocumentService.GetAllByCreator(authorID);
                    else if (curUserType == UserTypesEnum.SimpleUser)
                        documents = DocumentService.GetAllPurchasedByUser(purchasedUserID);
                    else
                        documents = DocumentService.GetAllApproved();
                }
                else
                    documents = DocumentService.GetAllApproved();

                recordsTotal = documents.Count();

                documents = ApplyFilters(documents, lawAreas, statusID, priceType);

                recordsFiltered = documents.Count();

                documents = ApplyOrder(documents, start, length);

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

        protected IQueryable<Document> ApplyFilters(
            IQueryable<Document> documents,
            int[] lawAreas,
            int? statusID,
            DocumentPriceTypesEnum? priceType)
        {
            if (lawAreas != null && lawAreas.Length > 0)
            {
                documents = documents
                    .Where(m => m.DocumentLawAreas
                        .Select(n => n.LawAreaID)
                        .Any(n => lawAreas.Contains(n)));
            }

            if (statusID.HasValue)
            {
                if (statusID.Value == (int)DocumentStatusesEnum.QuestionCompleted)
                {
                    documents = documents
                        .Where(m => m.DocumentStatusID == (int)DocumentStatusesEnum.Approved
                            && m.QuestionIsCompleted == true);
                }
                else if (statusID.Value == (int)DocumentStatusesEnum.QuestionCompletedWithFeeDistribution)
                {
                    documents = documents
                        .Where(m => m.DocumentStatusID == (int)DocumentStatusesEnum.Approved
                            && m.QuestionIsCompleted == true
                            && m.QuestionFeeIsDistributed == true);
                }
                else
                    documents = documents.Where(m => m.DocumentStatusID == statusID.Value);
            }
                
            if (priceType.HasValue)
                documents = documents.Where(m => m.PriceType == priceType.Value);

            return documents;
        }

        protected IQueryable<Document> ApplyOrder(
            IQueryable<Document> documents,
            int? start,
            int? length)
        {
            documents = documents.OrderByDescending(m => m.DateModified);
            if (start.HasValue && start.Value > 0)
                documents = documents.Skip(start.Value);
            if (length.HasValue && length.Value > 0)
                documents = documents.Take(length.Value);

            return documents;
        }
    }
}