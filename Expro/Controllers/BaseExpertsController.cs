using System;
using System.Collections.Generic;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace Expro.Controllers
{
    public class BaseExpertsController : BaseController
    {
        private readonly IExpertsListSearchService _expertsListSearchService;
        private readonly IUserService _userService;
        private readonly IExpertsListAdminActionsService _expertsListAdminActionsService;
        private readonly ILawAreaService _lawAreaService;
        private readonly IRegionService _regionService;

        public BaseExpertsController(
            IExpertsListSearchService expertsListSearchService,
            IUserService userService,
            IExpertsListAdminActionsService expertsListAdminActionsService,
            IStringLocalizer<Resources.ResourceTexts> localizer,
            ILawAreaService lawAreaService,
            IRegionService regionService
            )
        {
            _expertsListSearchService = expertsListSearchService;
            _userService = userService;
            _expertsListAdminActionsService = expertsListAdminActionsService;
            _localizer = localizer;
            _lawAreaService = lawAreaService;
            _regionService = regionService;
        }

        public virtual IActionResult Index()
        {
            ViewData["lawAreas"] = _lawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = false,
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            ViewData["regions"] = _regionService.GetAsSelectList();

            return View();
        }

        [HttpPost]
        public virtual IActionResult Search(
            int draw, int? start = null, int? length = null, 
            int? statusID = null,
            int? lawAreaParent = null,
            int[] lawAreas = null,
            int? regionID = null,
            int? cityID = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);

            IQueryable<ApplicationUser> dataIQueryable = _expertsListSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType,
                statusID,
                "",
                lawAreaParent,
                lawAreas,
                regionID,
                cityID);

            dynamic data = new ExpertListInfoVM().GetExpertListInfoVM(dataIQueryable);

            return Json(new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data,
                error = error
            });
        }

        public virtual IActionResult Details(string id)
        {
            var expert = _userService.GetWithRelatedDataByID(id);
            if (expert == null)
                throw new Exception("Эксперт не найден");

            ProfileExpertFullInfoVM profileExpertFullInfoVM = new ProfileExpertFullInfoVM(expert);

            

            return View(profileExpertFullInfoVM);
        }

        [HttpPost]
        public virtual IActionResult Approve(string id)
        {
            try
            {
                var expert = _userService.GetByID(id);
                if (expert == null)
                    throw new Exception("Эксперт не найден");

                _expertsListAdminActionsService.Approve(expert);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        [HttpPost]
        public virtual IActionResult Reject(string id)
        {
            try
            {
                var expert = _userService.GetByID(id);
                if (expert == null)
                    throw new Exception("Эксперт не найден");

                var curUser = accountUtil.GetCurrentUser(User);
                _expertsListAdminActionsService.Reject(expert);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        [HttpPost]
        public virtual IActionResult ApproveEmail(string id)
        {
            try
            {
                var expert = _userService.GetByID(id);
                if (expert == null)
                    throw new Exception("Эксперт не найден");

                _expertsListAdminActionsService.ApproveEmail(expert);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}