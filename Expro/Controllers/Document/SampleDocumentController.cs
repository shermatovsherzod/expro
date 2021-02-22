using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Expro.Controllers
{
    public class SampleDocumentController : BaseDocumentController
    {
        public SampleDocumentController(
            ISampleDocumentService sampleDocumentService,
            ISampleDocumentSearchService sampleDocumentSearchService,
            IUserBalanceService userBalanceService,
            IUserPurchasedPracticeDocumentService userPurchasedDocumentService,
            //UserManager<ApplicationUser> userManager,
            IUserService userService,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService,
            IUserRatingService userRatingService,
            IStringLocalizer<Resources.ResourceTexts> localizer)
            : base(
                  sampleDocumentService,
                  sampleDocumentSearchService,
                  userBalanceService,
                  userPurchasedDocumentService,
                  //userManager,
                  userService,
                  lawAreaService,
                  documentCounterService,
                  userRatingService,
                  localizer)
        {
            DocumentType = DocumentTypesEnum.SampleDocument.ToString();
            ErrorDocumentNotFound = _localizer["SampleDocumentNotFound"];
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

        [Authorize()]
        [HttpPost]
        public override IActionResult Purchase(DocumentPurchaseFormVM purchaseFormVM)
        {
            return base.Purchase(purchaseFormVM);
        }
    }
}
