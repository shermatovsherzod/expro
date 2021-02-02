using Expro.Models;
using Expro.Models.Enums;
using Expro.Utils;
using Expro.ViewModels;
using Expro.ViewModels.Expert;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class ExpertShortInfoViewComponent : ViewComponent
    {
        UserManager<ApplicationUser> _userManager;

        public ExpertShortInfoViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(string id = "")
        {
            if (String.IsNullOrEmpty(id))
            {
                AccountUtil accountUtil = new AccountUtil();
                var currentUserAccount = accountUtil.GetCurrentUser(this.HttpContext.User);
                var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);

                var currentUserInfo = new ExpertShortInfoVM(currentUser);

                return View("ExpertShortInfo", currentUserInfo);
            }


            var user = _userManager.Users.FirstOrDefault(c => c.Id == id);

            var userInfo = new ExpertShortInfoVM(user);

            return View("ExpertShortInfo", userInfo);

        }

    }
}
