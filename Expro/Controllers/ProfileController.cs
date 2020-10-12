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
    public class ProfileController : BaseController
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
            //var curUser = accountUtil.GetCurrentUser(User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == "sirius.gml@gmail.com");

            ExpertProfileEditVM profileEditVM = new ExpertProfileEditVM(user);
            
            ViewData["lawAreas"] = _lawAreaService.GetAsSelectList();
            ViewData["regions"] = _regionService.GetAsSelectListOne(user.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(user.CityID); // buni ham huddi registerdaka bo'ladide....
           

            return View(profileEditVM);
        }

        [HttpPost]
        public IActionResult UserProfile(string FirstName)
        {           
            ModelState.AddModelError("FirstName", "Сообщение об ошибке");
            return BadRequest(ModelState);
            return Ok();
                      
        }

        [HttpPost]
        public IActionResult Contacts()
        {
            string result = "Added ";

            return Ok();
        }

        [HttpPost]
        public IActionResult Education()
        {
            string result = "Added ";

            return Json(result);
        }

        [HttpPost]
        public IActionResult Experience()
        {
            string result = "Added ";

            return Json(result);
        }
    }
}