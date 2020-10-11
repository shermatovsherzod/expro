using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILawAreaService _lawAreaService;
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
        private readonly IGenderService _genderService;

        public ProfileController(
              UserManager<ApplicationUser> userManager,
              ILawAreaService lawAreaService,
              IRegionService regionService,
              ICityService cityService,
              IGenderService genderService
              )
        {
            _userManager = userManager;
            _lawAreaService = lawAreaService;
            _regionService = regionService;
            _cityService = cityService;
            _genderService = genderService;
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
            userVM.Email = user.Email;
            userVM.PhoneNumber = user.PhoneNumber;
            userVM.GenderID = user.GenderID;
            //userVM. = user.;
            //userVM. = user.;
            //userVM. = user.;

            ViewData["lawAreas"] = _lawAreaService.GetAsSelectList();
            ViewData["regions"] = _regionService.GetAsSelectListOne(user.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(user.CityID);
           

            return View(userVM);
        }

        [HttpPost]
        public JsonResult MainInfo(string FirstName)
        {
            string result = "Added ";

            return Json(new { success = false, failure = true }) ;
        }

        [HttpPost]
        public JsonResult Contacts()
        {
            string result = "Added ";

            return Json(result);
        }

        [HttpPost]
        public JsonResult Education()
        {
            string result = "Added ";

            return Json(result);
        }

        [HttpPost]
        public JsonResult Experience()
        {
            string result = "Added ";

            return Json(result);
        }
    }
}