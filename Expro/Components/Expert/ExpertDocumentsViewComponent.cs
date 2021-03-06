﻿using Expro.Models;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class ExpertDocumentsViewComponent : BaseViewComponent
    {
        private readonly IArticleDocumentService _articleDocumentService;
        private readonly IPracticeDocumentService _practiceDocumentService;
        private readonly ISampleDocumentService _sampleDocumentService;

        public ExpertDocumentsViewComponent(
            IArticleDocumentService articleDocumentService,
            IPracticeDocumentService practiceDocumentService,
            ISampleDocumentService sampleDocumentService)
            : base()
        {
            _articleDocumentService = articleDocumentService;
            _practiceDocumentService = practiceDocumentService;
            _sampleDocumentService = sampleDocumentService;
        }

        public override IViewComponentResult Invoke()
        {
            var curUser = accountUtil.GetCurrentUser(HttpContext.User);
            DocumentsCountVM vmodel = new DocumentsCountVM()
            {
                ArticleDocumentsCount = _articleDocumentService.GetAllByCreator(curUser.ID).Count(),
                PracticeDocumentsCount = _practiceDocumentService.GetAllByCreator(curUser.ID).Count(),
                SampleDocumentsCount = _sampleDocumentService.GetAllByCreator(curUser.ID).Count(),
            };

            return View("ExpertDocuments", vmodel);
        }
    }
}
