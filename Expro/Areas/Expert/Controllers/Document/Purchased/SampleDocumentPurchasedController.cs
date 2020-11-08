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
    public class SampleDocumentPurchasedController : BaseController
    {
        private readonly ISampleDocumentService SampleDocumentService;
        private readonly ISampleDocumentSearchService SampleDocumentSearchService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILawAreaService LawAreaService;
        private readonly IDocumentCounterService DocumentCounterService;

        public SampleDocumentPurchasedController(
            ISampleDocumentService sampleDocumentService,
            ISampleDocumentSearchService sampleDocumentSearchService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService)
        {
            SampleDocumentService = sampleDocumentService;
            SampleDocumentSearchService = sampleDocumentSearchService;
            _userManager = userManager;
            LawAreaService = lawAreaService;
            DocumentCounterService = documentCounterService;
        }

        public IActionResult Index()
        {
            ViewData["lawAreas"] = LawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = false,
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, 
            DocumentPriceTypesEnum? priceType = null,
            int[] lawAreas = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);
            ApplicationUser user = await _userManager.GetUserAsync(User);

            IQueryable<Document> dataIQueryable = SampleDocumentSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType.Value,
                statusID,
                priceType,
                curUser.ID,
                user,
                lawAreas
            );

            dynamic data = dataIQueryable
                .ToList()
                .Select(m => new DocumentListItemForUserVM(m))
                .ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data,
                error = error
            });
        }

        public IActionResult Details(int id)
        {
            var document = SampleDocumentService.GetByID(id);
            if (document == null)
                throw new Exception("Намунавий хужжат не найден");

            DocumentCounterService.IncrementNumberOfViews(document);

            DocumentDetailsForUserVM documentVM = new DocumentDetailsForUserVM(document);

            return View(documentVM);
        }
    }
}
