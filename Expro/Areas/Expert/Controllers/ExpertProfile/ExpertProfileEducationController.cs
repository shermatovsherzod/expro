using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Expro.Areas.Expert.Controllers.ExpertProfile
{
    [Area("Expert")]
    public class ExpertProfileEducationController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEducationService _educationService;
        private readonly ICountryService _countryService;

        public ExpertProfileEducationController(
              UserManager<ApplicationUser> userManager,           
              IEducationService educationService   ,
               ICountryService countryService
              )
        {
            _userManager = userManager;           
            _educationService = educationService;
            _countryService = countryService;


        }

        public IActionResult Index()
        {
            var user = accountUtil.GetCurrentUser(User);
            ViewData["country"] = _countryService.GetAsSelectList();
            ViewData["educationListVM"] = GetEducationListByUser(user.ID);

            return View();
            
        }


        [HttpPost]
        public IActionResult Index(ExpertProfileEducationEditVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);
            ViewData["country"] = _countryService.GetAsSelectList();

            if (ModelState.IsValid && user != null)
            {
                Education model = new Education();               
                PropertyCopier.CopyTo(vmodel, model);               
                _educationService.Add(model, user.ID);
                return RedirectToAction("Education");
            }
            ViewData["educationListVM"] = GetEducationListByUser(user.ID);
            return View(vmodel);
        }


        [HttpPost]
        public IActionResult DeleteEducation(int id)
        {
            try
            {
                var model = _educationService.GetByID(id);
                var user = accountUtil.GetCurrentUser(User);
                if (model.UserID == user.ID)
                {
                    _educationService.DeletePermanently(model);
                    return Json(new { success = true, responseText = "Education deleted" });
                }
                return Json(new { success = true, responseText = "Education not deleted" });
            }
            catch (Exception)
            {
                return Json(new { success = true, responseText = "Education not deleted" });
            }
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
                UserID = s.UserID
            }).ToList();
        }
    }
}