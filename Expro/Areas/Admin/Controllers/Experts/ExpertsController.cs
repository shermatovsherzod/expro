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
    public class ExpertsController : BaseExpertsController
    {
        private readonly IExpertStatusService _expertStatusService;        

        public ExpertsController(
             IExpertsListSearchService expertsListSearchService,
             IUserService userService,
             IExpertStatusService expertStatusService,
             IExpertsListAdminActionsService expertsListAdminActionsService,
             IStringLocalizer<Resources.ResourceTexts> localizer,
             ILawAreaService lawAreaService,
             IRegionService regionService
           ) : base(
               expertsListSearchService,
               userService,
               expertsListAdminActionsService,
               localizer,
               lawAreaService,
               regionService)
        {
            _expertStatusService = expertStatusService;       
        }

        public override IActionResult Index()
        {
            ViewData["statuses"] = _expertStatusService.GetAsSelectList();
            return base.Index();
        }

        [HttpPost]
        public override IActionResult Search(
            int draw, int? start = null, int? length = null, 
            int? statusID = null,
            int? lawAreaParent = null,
            int[] lawAreas = null,
            int? regionID = null,
            int? cityID = null)
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

        [HttpPost]
        public override IActionResult ApproveEmail(string id)
        {
            return base.ApproveEmail(id);
        }

        [HttpPost]
        public IActionResult Block(string id)
        {
            try
            {
                var appUser = _userService.GetByID(id);
                if (appUser == null)
                    throw new Exception("Пользователь не найден");

                _userService.Block(appUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Unblock(string id)
        {
            try
            {
                var appUser = _userService.GetByID(id);
                if (appUser == null)
                    throw new Exception("Пользователь не найден");

                _userService.Unblock(appUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}
