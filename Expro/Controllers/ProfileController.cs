using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(
              UserManager<ApplicationUser> userManager
              )
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return RedirectToAction("UserProfile");

            return View();
        }

        public IActionResult UserProfile()
        {
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == "sirius.gml@gmail.com");
            UserVM userVM = new UserVM();
            userVM.FirstName = user.FirstName;
            userVM.LastName = user.LastName;
            userVM.PatronymicName = user.PatronymicName;
            userVM.DateOfBirth = user.DateOfBirth;
            //userVM. = user.;
            //userVM. = user.;
            //userVM. = user.;
            //userVM. = user.;
            //userVM. = user.;
            //userVM. = user.;

            return View(userVM);
        }

        public IActionResult ExpertProfile()
        {
            return View();
        }

        public IActionResult AdminProfile()
        {
            return View();
        }
    }
}