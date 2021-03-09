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
using Expro.ViewModels.SimpleUser;

namespace Expro.Controllers
{
    public class BaseSimpleUsersController : BaseController
    {
        private readonly ISimpleUsersListSearchService _simpleUsersListSearchService;
        protected readonly IUserService _userService;
        private readonly ISimpleUsersListAdminActionsService _simpleUsersListAdminActionsService;       
        private readonly IRegionService _regionService;

        public BaseSimpleUsersController(
            ISimpleUsersListSearchService simpleUsersListSearchService,
            IUserService userService,
            ISimpleUsersListAdminActionsService simpleUsersListAdminActionsService,
            IStringLocalizer<Resources.ResourceTexts> localizer,          
            IRegionService regionService
            )
        {
            _simpleUsersListSearchService = simpleUsersListSearchService;
            _userService = userService;
            _simpleUsersListAdminActionsService = simpleUsersListAdminActionsService;
            _localizer = localizer;          
            _regionService = regionService;
        }

        public virtual IActionResult Index()
        {
            ViewData["regions"] = _regionService.GetAsSelectList();

            return View();
        }

        [HttpPost]
        public virtual IActionResult Search(
            int draw, int? start = null, int? length = null,          
            int? regionID = null,
            int? cityID = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);

            IQueryable<ApplicationUser> dataIQueryable = _simpleUsersListSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,
                curUser.UserType,              
                "",
                regionID,
                cityID);

            dynamic data = new SimpleUserListInfoVM().GetSimpleUserListInfoVM(dataIQueryable);

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
            var user = _userService.GetWithRelatedDataByID(id);
            if (user == null)
                throw new Exception("Пользователь не найден");

            ProfileSimpleUserFullInfoVM profileSimpleUserVM = new ProfileSimpleUserFullInfoVM(user);            

            return View(profileSimpleUserVM);
        }

      
        [HttpPost]
        public virtual IActionResult ApproveEmail(string id)
        {
            try
            {
                var user = _userService.GetByID(id);
                if (user == null)
                    throw new Exception("Пользователь не найден");

                _simpleUsersListAdminActionsService.ApproveEmail(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}