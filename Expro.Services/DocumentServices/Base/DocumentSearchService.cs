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
        private IDocumentService DocumentService;

        public DocumentSearchService() { }

        public DocumentSearchService(IDocumentService documentService)
        {
            DocumentService = documentService;
        }

        public IQueryable<Document> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,
            int? statusID,
            DocumentPriceTypesEnum? priceType,
            string authorID,
            ApplicationUser purchaser,
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
                    if (curUserType == UserTypesEnum.Admin)
                        documents = DocumentService.GetAllForAdmin();
                    else if (curUserType == UserTypesEnum.Expert)
                        documents = DocumentService.GetAllByCreator(authorID);
                    else if (curUserType == UserTypesEnum.SimpleUser)
                        documents = DocumentService.GetAllPurchasedByUser(purchaser);
                    else
                        documents = DocumentService.GetAllApproved();
                }
                else
                    documents = DocumentService.GetAllApproved();

                if (lawAreas != null && lawAreas.Length > 0)
                {
                    documents = documents
                        .Where(m => m.DocumentLawAreas
                            .Select(n => n.LawAreaID)
                            .Any(n => lawAreas.Contains(n)));
                }

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