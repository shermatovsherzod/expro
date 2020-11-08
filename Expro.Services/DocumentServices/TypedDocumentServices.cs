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
}