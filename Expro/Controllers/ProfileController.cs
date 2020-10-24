using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.ViewModels.Expert;
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
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);

            ViewData["country"] = _countryService.GetAsSelectList();
            ViewData["lawAreas"] = _lawAreaService.GetAsSelectList();
            ViewData["regions"] = _regionService.GetAsSelectListOne(currentUser.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(currentUser.CityID);
            ViewData["gender"] = _genderService.GetAsSelectListOne(currentUser.GenderID);

            var userMainInfo = new ExpertProfileMainInfoVM(currentUser);
            return View(userMainInfo);
        }

        [HttpPost]
        public async Task<ActionResult> MainInfo(ExpertProfileMainInfoVM vmodel)
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = await _userManager.FindByIdAsync(currentUserAccount.ID);// Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);

            ViewData["country"] = _countryService.GetAsSelectList();
            ViewData["lawAreas"] = _lawAreaService.GetAsSelectList();
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
                user.DateOfBirth = GetDate(vmodel.DateOfBirth);
                user.GenderID = vmodel.GenderID;
                _lawAreaService.UpdateUserLawAreas(user, vmodel.LawAreas);
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userMainInfo = new ExpertProfileMainInfoVM(user);
                    return View(userMainInfo);
                }
                else
                {
                    return View(vmodel);
                }
            }

            return View(vmodel);
        }

        public DateTime GetDate(string dateOfBirth)
        {
            return DateTime.Now;
        }


        public IActionResult Contacts()
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            var userContactInfo = new ExpertProfileContactVM(user);
            return View(userContactInfo);
        }

        [HttpPost]
        public async Task<ActionResult> Contacts(ExpertProfileContactVM vmodel)
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = await _userManager.FindByIdAsync(currentUserAccount.ID);

            if (ModelState.IsValid && user != null)
            {
                user.Email = vmodel.Email;
                user.PhoneNumber = vmodel.PhoneNumber;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userContactInfo = new ExpertProfileContactVM(user);
                    return View(userContactInfo);
                }
                else
                {
                    return View(vmodel);
                }
            }

            return View(vmodel);
        }


        public IActionResult Education()
        {
            var user = accountUtil.GetCurrentUser(User);
            ViewData["country"] = _countryService.GetAsSelectList();
            ViewData["educationListVM"] = GetEducationListByUser(user.ID);

            return View();
        }

        [HttpPost]
        public IActionResult Education(ExpertProfileEducationFormVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);
            ViewData["country"] = _countryService.GetAsSelectList();          

            if (ModelState.IsValid && user != null)
            {
                Education model = new Education();

                model.CountryID = vmodel.CountryID;
                model.University = vmodel.University;
                model.Faculty = vmodel.Faculty;
                model.GraduationYear = vmodel.GraduationYear;
                model.City = vmodel.City;
                model.UserID = user.ID;
                _educationService.Add(model, user.ID);
            }
            ViewData["educationListVM"] = GetEducationListByUser(user.ID);
            return View(vmodel);
        }

        public List<EducationListItemVM> GetEducationListByUser(string userID)
        {
            return _educationService.GetListByUserID(userID).Select(s => new EducationListItemVM
            {
                University = s.University,
                City = s.City,
                Country = s.Country.Name,
                Faculty = s.Faculty,
                GraduationYear = s.GraduationYear,
                ID = s.ID,
                UserID=s.UserID
            }).ToList();
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