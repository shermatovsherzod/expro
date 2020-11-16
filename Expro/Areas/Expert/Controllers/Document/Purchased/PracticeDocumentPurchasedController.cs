﻿using System;
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

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class PracticeDocumentPurchasedController : BaseDocumentPurchasedController
    {
        public PracticeDocumentPurchasedController(
            IPracticeDocumentService practiceDocumentService,
            IPracticeDocumentSearchService practiceDocumentSearchService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService)
            : base(
                  practiceDocumentService,
                  practiceDocumentSearchService,
                  userManager,
                  lawAreaService,
                  documentCounterService)
        {
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

        public override IActionResult Details(int id)
        {
            return base.Details(id);
        }
    }
}