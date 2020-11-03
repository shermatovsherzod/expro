using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    public class ExpertsListController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ExpertsListController(
           UserManager<ApplicationUser> userManager
           )
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var expertsList = _userManager.Users.Where(c => c.UserType == UserTypesEnum.Expert).ToList();

            List<AppUserVM> expertsListVM = new List<AppUserVM>();
            foreach (var item in expertsList)
            {
                expertsListVM.Add(new AppUserVM(item));
            }

            return View(expertsListVM);
        }
    }
}