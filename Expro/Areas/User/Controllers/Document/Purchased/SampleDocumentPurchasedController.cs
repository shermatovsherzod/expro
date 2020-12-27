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
    public class SampleDocumentPurchasedController : BaseDocumentPurchasedController
    {
        public SampleDocumentPurchasedController(
            ISampleDocumentService sampleDocumentService,
            ISampleDocumentSearchService sampleDocumentSearchService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService,
            IUserRatingService userRatingService)
            : base(
                  sampleDocumentService,
                  sampleDocumentSearchService,
                  userManager,
                  lawAreaService,
                  documentCounterService,
                  userRatingService)
        {
            ErrorDocumentNotFound = "Образцовый документ не найден";
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
            int[] lawAreas = null)
        {
            return base.Search(draw, start, length, statusID, priceType, lawAreas);
        }

        public override IActionResult Details(int id)
        {
            return base.Details(id);
        }
    }
}
