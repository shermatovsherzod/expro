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
        private readonly ICountryService _countryService;
        private readonly IEducationService _educationService;
        private readonly IWorkExperienceService _workExperienceService;

        public ProfileController(
              UserManager<ApplicationUser> userManager,
              ILawAreaService lawAreaService,
              IRegionService regionService,
              ICityService cityService,
              IGenderService genderService,
              ICountryService countryService,
              IEducationService educationService,
              IWorkExperienceService workExperienceService
              )
        {
            _userManager = userManager;
            _lawAreaService = lawAreaService;
            _regionService = regionService;
            _cityService = cityService;
            _genderService = genderService;
            _countryService = countryService;
            _educationService = educationService;
            _workExperienceService = workExperienceService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("MainInfo");
        }

        public IActionResult MainInfo()
        {
            //var currentUser = accountUtil.GetCurrentUser(User);

            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == "sirius.gml@gmail.com");

            ViewData["country"] = _countryService.GetAsSelectList();
            ViewData["lawAreas"] = _lawAreaService.GetAsSelectList();
            ViewData["regions"] = _regionService.GetAsSelectListOne(currentUser.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(currentUser.CityID);
            ViewData["gender"] = new[] { "Мужской", "Женский" };

            var userMainInfo = new ExpertProfileMainInfoVM(currentUser);
            return View(userMainInfo);
        }

        [HttpPost]
        public IActionResult MainInfo(ExpertProfileMainInfoVM vmodel, int userGender)
        {
            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == "sirius.gml@gmail.com");

            ViewData["country"] = _countryService.GetAsSelectList();
            ViewData["lawAreas"] = _lawAreaService.GetAsSelectList();
            ViewData["regions"] = _regionService.GetAsSelectListOne(vmodel.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(vmodel.CityID);
            if (ModelState.IsValid && currentUser != null)
            {
                currentUser.FirstName = vmodel.FirstName;
                currentUser.LastName = vmodel.LastName;
                currentUser.PatronymicName = vmodel.PatronymicName;
                currentUser.RegionID = vmodel.RegionID;
                currentUser.CityID = vmodel.CityID;
                currentUser.DateOfBirth = GetDate(vmodel.DateOfBirth);
                currentUser.GenderID = userGender;
                _userManager.UpdateAsync(currentUser);

                var userMainInfo = new ExpertProfileMainInfoVM(currentUser);
                return View(userMainInfo);
            }

            return View(vmodel);
        }

        public DateTime GetDate(string dateOfBirth)
        {

            return DateTime.Now;

        }


        public IActionResult Contacts()
        {
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == "sirius.gml@gmail.com");
            var userContactInfo = new ExpertProfileContactVM(user);
            return View(userContactInfo);
        }

        [HttpPost]
        public IActionResult Contacts(string Email)
        {
            string result = "Added ";

            return View();
        }


        public IActionResult Education()
        {
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == "sirius.gml@gmail.com");
            ViewData["educationListVM"] = _educationService.GetListByUserID(user.Id).Select(s => new EducationListItemVM
            {
                University = s.University,
                City = s.City,
                Country = s.Country.Name,
                Faculty = s.Faculty,
                GraduationYear = s.GraduationYear,
                ID = s.ID
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Education(string a)
        {


            return View();
        }


        public IActionResult Experience()
        {
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == "sirius.gml@gmail.com");
            ViewData["workExperienceListItemVM"] = _workExperienceService.GetListByUserID(user.Id).Select(s => new WorkExperienceListItemVM
            {
                PlaceOfWork = s.PlaceOfWork,
                Position = s.Position,
                WorkPeriodFrom = s.WorkPeriodFrom,
                WorkPeriodTo = s.WorkPeriodTo,
                City = s.City,
                Country = s.Country.Name,
                ID = s.ID
            }).ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Experience(string b)
        {

            return View();
        }
    }
}