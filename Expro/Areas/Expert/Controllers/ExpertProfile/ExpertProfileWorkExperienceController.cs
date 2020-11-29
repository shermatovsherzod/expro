using System;
using Expro.Controllers;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers.ExpertProfile
{
    [Area("Expert")]
    public class ExpertProfileWorkExperienceController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWorkExperienceService _workExperienceService;
        private readonly ICountryService _countryService;

        public ExpertProfileWorkExperienceController(
              UserManager<ApplicationUser> userManager,
              IWorkExperienceService workExperienceService,
               ICountryService countryService
              )
        {
            _userManager = userManager;
            _workExperienceService = workExperienceService;
            _countryService = countryService;
        }

        public IActionResult Index()
        {
            var user = accountUtil.GetCurrentUser(User);
            GetEducationViewData(user);
            ViewBag.Message = false;
            return View();
        }

        [HttpPost]
        public IActionResult Index(ExpertProfileWorkExperienceEditVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);
            if (ModelState.IsValid && user != null)
            {
                WorkExperience model = new WorkExperience();
                PropertyCopier.CopyTo(vmodel, model);
                _workExperienceService.Add(model, user.ID);
                GetEducationViewData(user);
                ViewBag.Message = true;
                return View();
            }
            return View(vmodel);
        }

        private void GetEducationViewData(AppUserVM user)
        {
            ViewData["country"] = _countryService.GetAsSelectList();
            ViewData["workExperienceEducationListVM"] = new ExpertProfileWorkExperienceDetailsVM().GetListOfExpertProfileWorkExperienceDetailsVM(_workExperienceService.GetExpertWorkExperienceByCreatorID(user.ID));
        }

        [HttpPost]
        public IActionResult DeleteWorkExperience(int id)
        {
            try
            {
                var model = _workExperienceService.GetByID(id);
                var user = accountUtil.GetCurrentUser(User);
                if (_workExperienceService.WorkExperienceBelongsToUser(model, user.ID))
                {
                    _workExperienceService.DeletePermanently(model);
                    return Ok();
                }
                throw new Exception("Что то пошло не так. Опыт не удален");
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}