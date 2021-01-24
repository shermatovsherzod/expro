using Expro.Models;
using Expro.Utils;
using Expro.ViewModels;
using Expro.ViewModels.SimpleUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class SimpleUserShortInfoViewComponent : ViewComponent
    {
        UserManager<ApplicationUser> _userManager;

        public SimpleUserShortInfoViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            AccountUtil accountUtil = new AccountUtil();
            var currentUserAccount = accountUtil.GetCurrentUser(this.HttpContext.User);
            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            SimpleUserShortInfoVM vmodel = new SimpleUserShortInfoVM(currentUser);

            return View("SimpleUserShortInfo", vmodel);
        }

    }
}
