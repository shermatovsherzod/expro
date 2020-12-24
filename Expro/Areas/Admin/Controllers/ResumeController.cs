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

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResumeController : BaseResumeController
    {  
        private readonly IResumeStatusService _resumeStatusService;

        public ResumeController(
             IResumeSearchService resumeSearchService,
             IResumeService resumeService,
             IResumeStatusService resumeStatusService,
             IResumeAdminActionsService resumeAdminActionsService
           ) : base(
               resumeSearchService,
               resumeService,
               resumeAdminActionsService
               )
        {
            _resumeStatusService = resumeStatusService;
        }

        public override IActionResult Index()
        {
            ViewData["statuses"] = _resumeStatusService.GetAsSelectList();
            return base.Index();
        }

        [HttpPost]
        public override IActionResult Search(int draw, int? start = null, int? length = null,  int? statusID = null)
        {
            return base.Search(draw, start, length, statusID);
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
