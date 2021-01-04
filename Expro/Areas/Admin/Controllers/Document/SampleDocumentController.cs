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
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class SampleDocumentController : BaseDocumentController
    {
        public SampleDocumentController(
            ISampleDocumentService sampleDocumentService,
            ISampleDocumentSearchService sampleDocumentSearchService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService,
            IDocumentAdminActionsService documentAdminActionsService,
            IUserService userService,
            IUserRatingService userRatingService)
            : base(
                  sampleDocumentService,
                  documentStatusService,
                  sampleDocumentSearchService,
                  documentAdminActionsService,
                  hangfireService,
                  userService,
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
