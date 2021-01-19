using System;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Common.Utilities;
using Expro.Controllers;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers.ExpertProfile
{
    [Area("Expert")]
    public class ExpertProfileMainInfoController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILawAreaService _lawAreaService;
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;

        public ExpertProfileMainInfoController(
              UserManager<ApplicationUser> userManager,
              ILawAreaService lawAreaService,
              IRegionService regionService,
              ICityService cityService,
              IGenderService genderService,
              ICountryService countryService
              )
        {
            _userManager = userManager;
            _lawAreaService = lawAreaService;
            _regionService = regionService;
            _cityService = cityService;
            _genderService = genderService;
            _countryService = countryService;
        }
        public IActionResult Index()
        {
            var userAccount = accountUtil.GetCurrentUser(User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == userAccount.UserName);
            ExpertProfileMainInfoViewData(user);
            ViewBag.Message = false;
            var userMainInfo = new ExpertProfileMainInfoEditVM(user);
            return View(userMainInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Index(ExpertProfileMainInfoEditVM vmodel)
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = await _userManager.FindByIdAsync(currentUserAccount.ID);

            ExpertProfileMainInfoViewData(user);
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
                _lawAreaService.UpdateUserLawAreas(user, vmodel.LawAreas);
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

        private void ExpertProfileMainInfoViewData(ApplicationUser user)
        {
            ViewData["country"] = _countryService.GetAsSelectList();         
            ViewData["regions"] = _regionService.GetAsSelectListOne(user.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(user.CityID);
            ViewData["gender"] = _genderService.GetAsSelectListOne(user.GenderID);           
            ViewData["lawAreas"] = _lawAreaService.GetAsGroupedSelectListForUser(user);
        }
    }
}