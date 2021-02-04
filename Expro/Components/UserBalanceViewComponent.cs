using Expro.Models;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class UserBalanceViewComponent : ViewComponent
    {
        UserManager<ApplicationUser> _userManager;
        public UserBalanceViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            AccountUtil accountUtil = new AccountUtil();
            var currentUserAccount = accountUtil.GetCurrentUser(this.HttpContext.User);
            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            BalanceVM vmodel = new BalanceVM();
            vmodel.Balance = currentUser.Balance;
            vmodel.AccountNumber = currentUser.AccountNumber;
            return View("UserBalance", vmodel);
        }
    }
}
