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
    public class SampleDocumentSearchService : DocumentSearchService, ISampleDocumentSearchService
    {
        public SampleDocumentSearchService(ISampleDocumentService sampleDocumentService)
            : base(sampleDocumentService)
        {

        }
    }

    public class ArticleDocumentSearchService : DocumentSearchService, IArticleDocumentSearchService
    {
        public ArticleDocumentSearchService(IArticleDocumentService articleDocumentService)
            : base(articleDocumentService)
        {

        }
    }

    public class PracticeDocumentSearchService : DocumentSearchService, IPracticeDocumentSearchService
    {
        public PracticeDocumentSearchService(IPracticeDocumentService practiceDocumentService)
            : base(practiceDocumentService)
        {

        }
    }

    public class QuestionDocumentSearchService : DocumentSearchService, IQuestionDocumentSearchService
    {
        public QuestionDocumentSearchService(IQuestionDocumentService questionDocumentService)
            : base(questionDocumentService)
        {

        }

        public override IQueryable<Document> Search(
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
                    //if (!string.IsNullOrWhiteSpace(purchasedUserID))
                    //    documents = DocumentService.GetAllPurchasedByUser(purchasedUserID);
                    //else if (curUserType == UserTypesEnum.Admin)
                    //    documents = DocumentService.GetAllForAdmin();
                    //else if (curUserType == UserTypesEnum.Expert)
                    //    documents = DocumentService.GetAllByCreator(authorID);
                    //else if (curUserType == UserTypesEnum.SimpleUser)
                    //    documents = DocumentService.GetAllPurchasedByUser(purchasedUserID);
                    //else
                    //    documents = DocumentService.GetAllApproved();

                    if (curUserType == UserTypesEnum.Admin)
                        documents = DocumentService.GetAllForAdmin();
                    else if (curUserType == UserTypesEnum.SimpleUser)
                        documents = DocumentService.GetAllByCreator(authorID);
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
    }
}