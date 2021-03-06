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
    public class ExpertDocumentsPurchasedViewComponent : BaseViewComponent
    {
        private readonly IQuestionService _questionService;
        private readonly IArticleDocumentService _articleDocumentService;
        private readonly IPracticeDocumentService _practiceDocumentService;
        private readonly ISampleDocumentService _sampleDocumentService;

        public ExpertDocumentsPurchasedViewComponent(
            IQuestionService questionService,
            IArticleDocumentService articleDocumentService,
            IPracticeDocumentService practiceDocumentService,
            ISampleDocumentService sampleDocumentService)
            : base()
        {
            _questionService = questionService;
            _articleDocumentService = articleDocumentService;
            _practiceDocumentService = practiceDocumentService;
            _sampleDocumentService = sampleDocumentService;
        }

        public override IViewComponentResult Invoke()
        {
            var curUser = accountUtil.GetCurrentUser(HttpContext.User);
            ExpertDocumentsPurchasedCountVM vmodel = new ExpertDocumentsPurchasedCountVM()
            {
                AnsweredQuestionsCount = _questionService.GetAllAnsweredByUser(curUser.ID).Count(),
                ArticleDocumentsCount = _articleDocumentService.GetAllPurchasedByUser(curUser.ID).Count(),
                PracticeDocumentsCount = _practiceDocumentService.GetAllPurchasedByUser(curUser.ID).Count(),
                SampleDocumentsCount = _sampleDocumentService.GetAllPurchasedByUser(curUser.ID).Count()
            };

            return View("ExpertDocumentsPurchased", vmodel);
        }
    }
}
