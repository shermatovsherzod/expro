using System;
using System.Collections.Generic;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    public class VacancyController : BaseController
    {
        private readonly IVacancyService _vacancyService;        
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;

        public VacancyController(
              UserManager<ApplicationUser> userManager,
              IVacancyService vacancyService,            
              IRegionService regionService,
              ICityService cityService
              )
        {           
            _vacancyService = vacancyService;           
            _regionService = regionService;
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            var user = accountUtil.GetCurrentUser(User);
            ViewData["vacancyListVM"] = new VacancyDetailsVM().GetListOfVacancyDetailsVM(_vacancyService.GetVacancyByCreatorID(user.ID));
            return View();
        }

        public IActionResult Create()
        {
            GetVacancyViewDataForCreate();
            return View();
        }

        [HttpPost]
        public IActionResult Create(VacancyEditVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);
            GetVacancyViewDataForCreate();
            if (ModelState.IsValid && user != null)
            {
                Vacancy model = new Vacancy();
                model.VacancyStatusID = (int)VacancyStatusEnum.WaitingForApproval;
                PropertyCopier.CopyTo(vmodel, model);

                _vacancyService.Add(model, user.ID);

                return RedirectToAction("Index");
            }
            return View(vmodel);
        }

        public void GetVacancyViewDataForCreate()
        {
            ViewData["regions"] = _regionService.GetAsSelectList();
            ViewData["cities"] = _cityService.GetAsSelectList();
        }


        public ActionResult Edit(int id)
        {
            var user = accountUtil.GetCurrentUser(User);
            if (!_vacancyService.VacancyBelongsToUser(id, user.ID))
            {
                throw new Exception("Редактирование невозможно");
            }

            VacancyEditVM vmodel = new VacancyEditVM(_vacancyService.GetByID(id));
            GetVacancyViewDataForEdit(vmodel);
            return View(vmodel);
        }

        [HttpPost]
        public ActionResult Edit(VacancyEditVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);

            if (!_vacancyService.VacancyBelongsToUser(vmodel.ID, user.ID))
            {
                throw new Exception("Редактирование невозможно");
            }

            GetVacancyViewDataForEdit(vmodel);
            
            if (ModelState.IsValid && user != null)
            {
                Vacancy model = _vacancyService.GetActiveByID(vmodel.ID);
                model.VacancyStatusID = (int)VacancyStatusEnum.WaitingForApproval;
                PropertyCopier.CopyTo(vmodel, model);
                _vacancyService.Update(model);

                return RedirectToAction("Index");
            }
            return View(vmodel);
        }

        public void GetVacancyViewDataForEdit(VacancyEditVM vmodel)
        {
            ViewData["regions"] = _regionService.GetAsSelectListOne(vmodel.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(vmodel.CityID);
        }

        public ActionResult Details(int id)
        {
            VacancyDetailsVM vmodel = new VacancyDetailsVM(_vacancyService.GetByID(id));
            return View(vmodel);
        }

        public ActionResult Delete(int id)
        {
            VacancyDetailsVM vmodel = new VacancyDetailsVM(_vacancyService.GetByID(id));
            return View(vmodel);
        }

        [HttpPost]
        public IActionResult Delete(VacancyDetailsVM vmodel)
        {
            try
            {
                var model = _vacancyService.GetByID(vmodel.ID);
                var user = accountUtil.GetCurrentUser(User);
                if (_vacancyService.VacancyBelongsToUser(model, user.ID))
                {
                    _vacancyService.DeletePermanently(model);
                    return RedirectToAction("Index");
                }
                throw new Exception("Что то пошло не так. Вакансия не удалена");
            }
            catch (Exception ex)
            {
                throw new Exception("Что то пошло не так. Вакансия не удалена");
            }
        }      
    }
}