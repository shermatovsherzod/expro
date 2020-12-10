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
    public class SampleDocumentService : DocumentService, ISampleDocumentService
    {
        public SampleDocumentService(IDocumentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _documentType = DocumentTypesEnum.SampleDocument;
        }
    }

    public class ArticleDocumentService : DocumentService, IArticleDocumentService
    {
        public ArticleDocumentService(IDocumentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _documentType = DocumentTypesEnum.ArticleDocument;
        }
    }

    public class PracticeDocumentService : DocumentService, IPracticeDocumentService
    {
        public PracticeDocumentService(IDocumentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _documentType = DocumentTypesEnum.PracticeDocument;
        }
    }

    public class QuestionDocumentService : DocumentService, IQuestionDocumentService
    {

        public QuestionDocumentService(IDocumentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _documentType = DocumentTypesEnum.QuestionDocument;
        }

        public void CompleteWithDistribution(Document question, string userID)
        {
            question.QuestionFeeIsDistributed = true;
            Complete(question, userID);
        }

        public void Complete(Document question, string userID)
        {
            question.QuestionIsCompleted = true;
            question.DateQuestionCompleted = DateTime.Now;

            Update(question, userID);
        }

        public bool AdminIsAllowedToComplete(Document question)
        {
            var now = DateTime.Now;

            if (!question.DateApproved.HasValue)
                return false;

            //if (question.DateApproved.Value.AddDays(3) > now)
            if (question.DateApproved.Value.AddMinutes(10) > now)
                return true;

            return false;
        }

        public bool IsCompleted(Document question)
        {
            return question.QuestionIsCompleted.HasValue ? 
                question.QuestionIsCompleted.Value : false;
        }
    }
}