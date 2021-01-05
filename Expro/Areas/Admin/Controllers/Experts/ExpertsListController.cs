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

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ExpertsListController : BaseExpertsListController
    {
        private readonly IExpertStatusService _expertStatusService;
        
        public ExpertsListController(
             IExpertsListSearchService expertsListSearchService,
             IUserService userService,
             IExpertStatusService expertStatusService,
             IExpertsListAdminActionsService expertsListAdminActionsService,
             IStringLocalizer<Resources.ResourceTexts> localizer
           ) : base(
               expertsListSearchService,
               userService,
               expertsListAdminActionsService,
               localizer
               )
        {
            _expertStatusService = expertStatusService;
        }

        public override IActionResult Index()
        {
            ViewData["statuses"] = _expertStatusService.GetAsSelectList();
            return base.Index();
        }

        [HttpPost]
        public override IActionResult Search(int draw, int? start = null, int? length = null, int? statusID = null)
        {
            return base.Search(draw, start, length, statusID);
        }


        public override IActionResult Details(string id)
        {
            return base.Details(id);
        }

        [HttpPost]
        public override IActionResult Approve(string id)
        {
            return base.Approve(id);
        }

        [HttpPost]
        public override IActionResult Reject(string id)
        {
            return base.Reject(id);
        }
    }
}
