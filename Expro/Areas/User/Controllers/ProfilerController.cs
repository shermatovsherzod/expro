using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common.Utilities;
using Expro.Controllers;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.ViewModels.Expert;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    public class ProfileController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;       
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;

        public ProfileController(
            UserManager<ApplicationUser> userManager,          
            IRegionService regionService,
            ICityService cityService,
            IGenderService genderService,
            ICountryService countryService         
            )
        {
            _userManager = userManager;         
            _regionService = regionService;
            _cityService = cityService;
            _genderService = genderService;
            _countryService = countryService;           
        }

        public IActionResult Index(string message = "")
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);

            ViewData["country"] = _countryService.GetAsSelectList();          
            ViewData["regions"] = _regionService.GetAsSelectListOne(currentUser.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(currentUser.CityID);
            ViewData["gender"] = _genderService.GetAsSelectListOne(currentUser.GenderID);
            ViewBag.Message = message;
            var userMainInfo = new ProfileSimpleUserVM(currentUser);
            return View(userMainInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProfileSimpleUserVM vmodel)
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = await _userManager.FindByIdAsync(currentUserAccount.ID);

            ViewData["country"] = _countryService.GetAsSelectList();          
            ViewData["regions"] = _regionService.GetAsSelectListOne(vmodel.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(vmodel.CityID);
            ViewData["gender"] = _genderService.GetAsSelectListOne(vmodel.GenderID);
            if (ModelState.IsValid && user != null)
            {
                user.FirstName = vmodel.FirstName;
                user.LastName = vmodel.LastName;
                user.PatronymicName = vmodel.PatronymicName;
                user.RegionID = vmodel.RegionID;
                user.CityID = vmodel.CityID;
                user.DateOfBirth = DateTimeUtils.ConvertToDateTime(vmodel.DateOfBirth, "dd.MM.yyyy");
                user.GenderID = vmodel.GenderID;             
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", new { message = "Изменения сохранены" });
                }
                else
                {
                    return View(vmodel);
                }
            }

            return View(vmodel);
        }
    }
}