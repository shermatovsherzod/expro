using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(
            UserManager<ApplicationUser> userManager
            )
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmationRequest()
        {
            AccountUtil accountUtil = new AccountUtil();
            var currentUserAccount = accountUtil.GetCurrentUser(this.HttpContext.User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            if ((user.ApproveStatus == (int)ExpertApproveStatusEnum.NotApproved) || (user.ApproveStatus == (int)ExpertApproveStatusEnum.Rejected))
            {
                user.ApproveStatus = (int)ExpertApproveStatusEnum.WaitingForApproval;
                user.DateSubmittedForApproval = DateTime.Now;
                IdentityResult result = await _userManager.UpdateAsync(user);
                return Json(new { success = true });
            }
            return Json(new { error = true });
          
        }
    }
}