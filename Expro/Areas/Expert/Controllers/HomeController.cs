using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Policy = "ExpertOnly")]
    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStatusService _userStatusService;
        public HomeController(
            UserManager<ApplicationUser> userManager,
            IUserStatusService userStatusService
            )
        {
            _userManager = userManager;
            _userStatusService = userStatusService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProfileConfirmationRequestByExpert()
        {
            AccountUtil accountUtil = new AccountUtil();
            var currentUserAccount = accountUtil.GetCurrentUser(this.HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            if (((user.UserStatusID == (int)ExpertApproveStatusEnum.NotApproved) || (user.UserStatusID == (int)ExpertApproveStatusEnum.Rejected)) && await _userStatusService.ProfileConfirmationRequestByExpert(user.Id))
            {
                return Json(new { success = true });
            }

            throw new Exception("Ошибка. Что то пошло не так.");
        }
    }
}