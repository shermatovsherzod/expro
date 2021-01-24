using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Common.Utilities;
using Expro.Controllers;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Expro.ViewModels.Expert;
using Expro.ViewModels.SimpleUser;
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
       
        public IActionResult Index()
        {
            var userAccount = accountUtil.GetCurrentUser(User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == userAccount.UserName);
            UserProfileViewData(user);
            ViewBag.Message = false;
            var userMainInfo = new ProfileSimpleUserVM(user);
            return View(userMainInfo);
        }


        [HttpPost]
        public async Task<ActionResult> Index(ProfileSimpleUserVM vmodel)
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = await _userManager.FindByIdAsync(currentUserAccount.ID);

            UserProfileViewData(user);
            ViewBag.Message = false;

            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            if (ModelState.IsValid)
            {
                user.FirstName = vmodel.FirstName;
                user.LastName = vmodel.LastName;
                user.PatronymicName = vmodel.PatronymicName;
                user.RegionID = vmodel.RegionID;
                user.CityID = vmodel.CityID;
                user.CityOther = vmodel.CityOther;
                user.DateOfBirth = DateTimeUtils.ConvertToDateTime(vmodel.DateOfBirth, AppData.Configuration.DateViewStringFormat);
                user.GenderID = vmodel.GenderID;              
                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    ViewBag.Message = true;
                    return View(vmodel);
                }
                throw new Exception("Изменения не сохранены. Что то пошло не так!");
            }

            return View(vmodel);
        }


        private void UserProfileViewData(ApplicationUser user)
        {
            ViewData["country"] = _countryService.GetAsSelectList();
            ViewData["regions"] = _regionService.GetAsSelectListOne(user.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(user.CityID);
            ViewData["gender"] = _genderService.GetAsSelectListOne(user.GenderID);          
        }
    }
}