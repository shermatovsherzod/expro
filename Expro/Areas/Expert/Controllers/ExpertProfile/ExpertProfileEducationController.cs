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
        private readonly IExpertEducationService _expertEducationService;
        private readonly ICountryService _countryService;

        public ExpertProfileEducationController(
              UserManager<ApplicationUser> userManager,
              IExpertEducationService expertEducationService,
               ICountryService countryService
              )
        {
            _userManager = userManager;
            _expertEducationService = expertEducationService;
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
        public IActionResult Index(ExpertProfileEducationEditVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);
            if (ModelState.IsValid && user != null)
            {
                ExpertEducation model = new ExpertEducation();
                PropertyCopier.CopyTo(vmodel, model);
                _expertEducationService.Add(model, user.ID);
                GetEducationViewData(user);
                ViewBag.Message = true;
                return View();
            }
            return View(vmodel);
        }

        private void GetEducationViewData(AppUserVM user)
        {
            ViewData["country"] = _countryService.GetAsSelectList();
            ViewData["expertEducationListVM"] = new ExpertProfileEducationDetailsVM().GetListOfExpertProfileEducationDetailsVM(_expertEducationService.GetExpertEducationsByCreatorID(user.ID));
        }

        [HttpPost]
        public IActionResult DeleteEducation(int id)
        {
            try
            {
                var model = _expertEducationService.GetByID(id);
                var user = accountUtil.GetCurrentUser(User);
                if (_expertEducationService.EducationBelongsToUser(model, user.ID))
                {
                    _expertEducationService.DeletePermanently(model);
                    return Ok();                    
                }              
                throw new Exception("Что то пошло не так. Образование не удалено");
            }
            catch (Exception ex)
            {                
                return CustomBadRequest(ex);               
            }
        }
    }
}