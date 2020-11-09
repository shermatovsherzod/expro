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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Controllers
{
    public class PracticeDocumentController : BaseDocumentController
    {
        public PracticeDocumentController(
            IPracticeDocumentService practiceDocumentService,
            IPracticeDocumentSearchService practiceDocumentSearchService,
            IUserBalanceService userBalanceService,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService)
            : base(
                  practiceDocumentService,
                  practiceDocumentSearchService,
                  userBalanceService,
                  userPurchasedDocumentService,
                  userManager,
                  lawAreaService,
                  documentCounterService)
        {
            DocumentType = DocumentTypesEnum.PracticeDocument.ToString();
            ErrorDocumentNotFound = "Практический документ не найден";
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

        public override async Task<IActionResult> Details(int id)
        {
            return await base.Details(id);
        }

        [HttpPost]
        public override async Task<IActionResult> Purchase(DocumentPurchaseFormVM purchaseFormVM)
        {
            return await base.Purchase(purchaseFormVM);
        }
    }
}
