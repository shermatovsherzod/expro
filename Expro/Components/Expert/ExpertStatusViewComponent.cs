using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
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
    public class ExpertStatusViewComponent : ViewComponent
    {
        UserManager<ApplicationUser> _userManager;
        private readonly IUserStatusService _userStatusService;

        public ExpertStatusViewComponent(UserManager<ApplicationUser> userManager, IUserStatusService userStatusService)
        {
            _userManager = userManager;
            _userStatusService = userStatusService;
        }

        public IViewComponentResult Invoke()
        {
            AccountUtil accountUtil = new AccountUtil();
            var currentUserAccount = accountUtil.GetCurrentUser(this.HttpContext.User);
            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);

            if (currentUser == null)
            {
                throw new Exception("Пользователь не найден");
            }
            ExpertStatusVM vmodel = new ExpertStatusVM(currentUser);

            return View("ExpertStatus", vmodel);

        }

    }
}
