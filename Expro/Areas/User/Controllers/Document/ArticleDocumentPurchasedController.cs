using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    public class ArticleDocumentPurchasedController : BaseDocumentPurchasedController
    {
        public ArticleDocumentPurchasedController(
            IArticleDocumentService articleDocumentService,
            IArticleDocumentSearchService articleDocumentSearchService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService)
            : base(
                  articleDocumentService,
                  articleDocumentSearchService,
                  userManager,
                  lawAreaService,
                  documentCounterService)
        {
            ErrorDocumentNotFound = "Статья не найдена";
            //Area = UserAreasEnum.User;
        }

        public override IActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public override async Task<IActionResult> Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, 
            DocumentPriceTypesEnum? priceType = null,
            int[] lawAreas = null)
        {
            return await base.Search(draw, start, length, statusID, priceType, lawAreas);
        }

        public override IActionResult Details(int id)
        {
            return base.Details(id);
        }
    }
}
