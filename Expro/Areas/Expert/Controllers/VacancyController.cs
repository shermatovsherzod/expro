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

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class VacancyController : BaseController
    {
        private readonly IVacancyService _vacancyService;
        private readonly ILawAreaService _lawAreaService;
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;

        public VacancyController(
              IVacancyService vacancyService,
              ILawAreaService lawAreaService,
              IRegionService regionService,
              ICityService cityService
              )
        {           
            _vacancyService = vacancyService;
            _lawAreaService = lawAreaService;
            _regionService = regionService;
            _cityService = cityService;
        }

        public IActionResult Index(bool? successfullyCreated = null)
        {
            ViewData["successfullyCreated"] = successfullyCreated;

            var user = accountUtil.GetCurrentUser(User);

            var vacancies = _vacancyService.GetVacancyByCreatorID(user.ID);

            return View(new VacancyDetailsVM().GetListOfVacancyDetailsVM(vacancies));
        }

        public IActionResult Create()
        {
            GetVacancyViewDataForCreate();
            return View();
        }

        [HttpPost]
        public IActionResult Create(VacancyEditVM vmodel)
        {
            try
            {
                var user = accountUtil.GetCurrentUser(User);
                //GetVacancyViewDataForCreate();
                if (ModelState.IsValid && user != null)
                {
                    Vacancy model = new Vacancy();
                    model.VacancyStatusID = (int)VacancyStatusEnum.WaitingForApproval;
                    PropertyCopier.CopyTo(vmodel, model);

                    _vacancyService.Add(model, user.ID);

                    if (vmodel.ActionType == DocumentActionTypesEnum.submitForApproval)
                        _vacancyService.SubmitForApproval(model, user.ID);

                    //return RedirectToAction("Index");
                    return Ok(new { id = model.ID });
                }
                else
                    //throw new Exception("Неверные данные");
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        public void GetVacancyViewDataForCreate()
        {
            ViewData["regions"] = _regionService.GetAsSelectList();
            ViewData["cities"] = _cityService.GetAsSelectList();
        }


        public IActionResult Edit(int id, bool? successfullySaved = null)
        {
            var user = accountUtil.GetCurrentUser(User);
            if (!_vacancyService.VacancyBelongsToUser(id, user.ID))
            {
                throw new Exception("Редактирование невозможно");
            }

            VacancyEditVM vmodel = new VacancyEditVM(_vacancyService.GetByID(id));
            GetVacancyViewDataForEdit(vmodel);

            ViewData["successfullySaved"] = successfullySaved;

            return View(vmodel);
        }

        [HttpPost]
        public IActionResult Edit(VacancyEditVM vmodel)
        {
            try
            {
                var user = accountUtil.GetCurrentUser(User);

                if (!_vacancyService.VacancyBelongsToUser(vmodel.ID, user.ID))
                {
                    throw new Exception("Редактирование невозможно");
                }

                //GetVacancyViewDataForEdit(vmodel);
            
                if (ModelState.IsValid && user != null)
                {
                    Vacancy model = _vacancyService.GetActiveByID(vmodel.ID);
                    model.VacancyStatusID = (int)VacancyStatusEnum.WaitingForApproval;
                    PropertyCopier.CopyTo(vmodel, model);

                    if (vmodel.ActionType == DocumentActionTypesEnum.submitForApproval)
                    {
                        _vacancyService.SubmitForApproval(model, user.ID);

                        //vmodel.StatusID = (int)DocumentStatusesEnum.WaitingForApproval;
                    }
                    else
                        _vacancyService.Update(model, user.ID);

                    return Ok();
                }
                else
                    //throw new Exception("Неверные данные");
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        public void GetVacancyViewDataForEdit(VacancyEditVM vmodel)
        {
            ViewData["regions"] = _regionService.GetAsSelectListOne(vmodel.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(vmodel.CityID);
        }

        //public ActionResult Details(int id)
        //{
        //    VacancyDetailsVM vmodel = new VacancyDetailsVM(_vacancyService.GetByID(id));
        //    return View(vmodel);
        //}

        //public ActionResult Delete(int id)
        //{
        //    VacancyDetailsVM vmodel = new VacancyDetailsVM(_vacancyService.GetByID(id));
        //    return View(vmodel);
        //}

        //[HttpPost]
        //public IActionResult Delete(VacancyDetailsVM vmodel)
        //{
        //    try
        //    {
        //        var model = _vacancyService.GetByID(vmodel.ID);
        //        var user = accountUtil.GetCurrentUser(User);
        //        if (_vacancyService.VacancyBelongsToUser(model, user.ID))
        //        {
        //            _vacancyService.DeletePermanently(model);
        //            return RedirectToAction("Index");
        //        }
        //        throw new Exception("Что то пошло не так. Вакансия не удалена");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Что то пошло не так. Вакансия не удалена");
        //    }
        //}      
    }
}