using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Policy = "SimpleUserOnly")]
    public class ArticleDocumentPurchasedController : BaseDocumentPurchasedController
    {
        public ArticleDocumentPurchasedController(
            IArticleDocumentService articleDocumentService,
            IArticleDocumentSearchService articleDocumentSearchService,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService,
            IStringLocalizer<Resources.ResourceTexts> localizer)
            : base(
                  articleDocumentService,
                  articleDocumentSearchService,
                  lawAreaService,
                  documentCounterService,
                  localizer)
        {
            ErrorDocumentNotFound = _localizer["ArticleNotFound"];
            //Area = UserAreasEnum.User;
        }

        public override IActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public override IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, 
            DocumentPriceTypesEnum? priceType = null,
            int? lawAreaParent = null,
            int[] lawAreas = null)
        {
            return base.Search(draw, start, length, statusID, priceType, lawAreaParent, lawAreas);
        }

        public override IActionResult Details(int id)
        {
            return base.Details(id);
        }
    }
}
