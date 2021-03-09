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
    public class SimpleUsersController : BaseSimpleUsersController
    {
        public SimpleUsersController(
             ISimpleUsersListSearchService simpleUsersListSearchService,
             IUserService userService,
             ISimpleUsersListAdminActionsService simpleUsersListAdminActionsService,
             IStringLocalizer<Resources.ResourceTexts> localizer,
             IRegionService regionService
           ) : base(
               simpleUsersListSearchService,
               userService,
               simpleUsersListAdminActionsService,
               localizer,
               regionService)
        {

        }

        public override IActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public override IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? regionID = null,
            int? cityID = null)
        {
            return base.Search(draw, start, length);
        }


        public override IActionResult Details(string id)
        {
            return base.Details(id);
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
