﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleDocumentController : BaseDocumentController
    {
        public ArticleDocumentController(
            IArticleDocumentService articleDocumentService,
            IArticleDocumentSearchService articleDocumentSearchService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService,
            IDocumentAdminActionsService documentAdminActionsService)
            : base(
                  articleDocumentService,
                  documentStatusService,
                  articleDocumentSearchService,
                  documentAdminActionsService,
                  hangfireService)
        {
            ErrorDocumentNotFound = "Статья не найдена";
        }

        public override IActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public override IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, DocumentPriceTypesEnum? priceType = null)
        {
            return base.Search(draw, start, length, statusID, priceType);
        }

        public override IActionResult Details(int id)
        {
            return base.Details(id);
        }

        [HttpPost]
        public override IActionResult Approve(int id)
        {
            return base.Approve(id);
        }

        [HttpPost]
        public override IActionResult Reject(int id)
        {
            return base.Reject(id);
        }
    }
}
