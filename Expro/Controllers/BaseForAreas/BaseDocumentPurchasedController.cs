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

namespace Expro.Controllers
{
    [Authorize(Policy = "ExpertOrSimpleUserOnly")]
    public class BaseDocumentPurchasedController : BaseController
    {
        private readonly IDocumentService DocumentService;
        private readonly IDocumentSearchService DocumentSearchService;
        private readonly ILawAreaService LawAreaService;
        private readonly IDocumentCounterService DocumentCounterService;

        protected string ErrorDocumentNotFound = "";
        //protected UserAreasEnum Area;

        public BaseDocumentPurchasedController(
            IDocumentService documentService,
            IDocumentSearchService documentSearchService,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService,
            IStringLocalizer<Resources.ResourceTexts> localizer)
        {
            DocumentService = documentService;
            DocumentSearchService = documentSearchService;
            LawAreaService = lawAreaService;
            DocumentCounterService = documentCounterService;
            _localizer = localizer;

            ErrorDocumentNotFound = _localizer["DocumentNotFound"];
        }

        public virtual IActionResult Index()
        {
            ViewData["lawAreas"] = LawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = false,
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            //ViewData["area"] = Area;

            return View();
        }

        [HttpPost]
        public virtual IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, 
            DocumentPriceTypesEnum? priceType = null,
            int? lawAreaParent = null,
            int[] lawAreas = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);
            //ApplicationUser user = await _userManager.GetUserAsync(User);

            IQueryable<Document> dataIQueryable = DocumentSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType.Value,
                (int)DocumentStatusesEnum.Approved,//statusID,
                priceType,
                curUser.ID,
                curUser.ID,
                lawAreaParent,
                lawAreas
            );

            dynamic data = dataIQueryable
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

        public virtual IActionResult Details(int id)
        {
            var document = DocumentService.GetByID(id);
            if (document == null)
                throw new Exception(ErrorDocumentNotFound);

            //ApplicationUser expert = document.Creator;
            //int points = DocumentService.PointsForDocumentFreeView;
            //_userRatingService.AddPointsToUser(points, expert);

            DocumentCounterService.IncrementNumberOfViews(document);

            DocumentDetailsForUserVM documentVM = new DocumentDetailsForUserVM(document);

            return View(documentVM);
        }
    }
}
