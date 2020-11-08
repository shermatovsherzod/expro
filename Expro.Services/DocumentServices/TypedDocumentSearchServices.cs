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
}