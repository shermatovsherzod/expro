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
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly ILawAreaService _lawAreaService;
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;

        public CompanyController(
              ICompanyService companyService,
              ILawAreaService lawAreaService,
              IRegionService regionService,
              ICityService cityService
              )
        {
            _companyService = companyService;
            _lawAreaService = lawAreaService;
            _regionService = regionService;
            _cityService = cityService;
        }

        public IActionResult Index(bool? successfullyCreated = null)
        {
            ViewData["successfullyCreated"] = successfullyCreated;

            var user = accountUtil.GetCurrentUser(User);

            var companies = _companyService.GetCompanyByCreatorID(user.ID);

            return View(new CompanyDetailsVM().GetListOfCompanyDetailsVM(companies));
        }

        public IActionResult Create()
        {
            GetCompanyViewDataForCreate();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CompanyEditVM vmodel)
        {
            try
            {
                var user = accountUtil.GetCurrentUser(User);
                //GetCompanyViewDataForCreate();
                if (ModelState.IsValid && user != null)
                {
                    Company model = new Company();
                    model.CompanyStatusID = (int)CompanyStatusEnum.NotApproved;
                    PropertyCopier.CopyTo(vmodel, model);

                    _lawAreaService.UpdateCompanyLawAreas(model, vmodel.LawAreas);
                    _companyService.Add(model, user.ID);

                    if (vmodel.ActionType == DocumentActionTypesEnum.submitForApproval)
                        _companyService.SubmitForApproval(model, user.ID);

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

        public void GetCompanyViewDataForCreate()
        {
            ViewData["lawAreas"] = _lawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            ViewData["regions"] = _regionService.GetAsSelectList();
            ViewData["cities"] = _cityService.GetAsSelectList();
        }


        public IActionResult Edit(int id, bool? successfullySaved = null)
        {
            var user = accountUtil.GetCurrentUser(User);
            if (!_companyService.CompanyBelongsToUser(id, user.ID))
            {
                throw new Exception("Редактирование невозможно");
            }

            CompanyEditVM vmodel = new CompanyEditVM(_companyService.GetByID(id));
            //GetCompanyViewDataForEdit(vmodel);
            GetCompanyViewDataForEdit();

            ViewData["successfullySaved"] = successfullySaved;

            return View(vmodel);
        }

        [HttpPost]
        public IActionResult Edit(CompanyEditVM vmodel)
        {
            try
            {
                var user = accountUtil.GetCurrentUser(User);

                if (!_companyService.CompanyBelongsToUser(vmodel.ID, user.ID))
                {
                    throw new Exception("Редактирование невозможно");
                }

                //GetCompanyViewDataForEdit(vmodel);

                if (ModelState.IsValid && user != null)
                {
                    Company model = _companyService.GetActiveByID(vmodel.ID);
                    PropertyCopier.CopyTo(vmodel, model);
                    _lawAreaService.UpdateCompanyLawAreas(model, vmodel.LawAreas);

                    if (vmodel.ActionType == DocumentActionTypesEnum.submitForApproval)
                    {
                        _companyService.SubmitForApproval(model, user.ID);

                        //vmodel.StatusID = (int)DocumentStatusesEnum.WaitingForApproval;
                    }
                    else
                        _companyService.Update(model, user.ID);

                    //return RedirectToAction("Index");
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

        //public void GetCompanyViewDataForEdit(CompanyEditVM vmodel)
        //{
        //    ViewData["lawAreas"] = _lawAreaService.GetAsIQueryable()
        //        .Select(m => new SelectListItemWithParent()
        //        {
        //            Value = m.ID.ToString(),
        //            Text = m.Name,
        //            Selected = vmodel.LawAreas.Contains(m.ID),
        //            ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
        //        }).ToList();

        //    ViewData["regions"] = _regionService.GetAsSelectListOne(vmodel.RegionID);
        //    ViewData["cities"] = _cityService.GetAsSelectListOne(vmodel.CityID);
        //}

        public void GetCompanyViewDataForEdit()
        {
            ViewData["lawAreas"] = _lawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    //Selected = vmodel.LawAreas.Contains(m.ID),
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            ViewData["regions"] = _regionService.GetAsSelectListOne();
            ViewData["cities"] = _cityService.GetAsSelectListOne();
        }



        //public ActionResult Details(int id)
        //{
        //    CompanyDetailsVM vmodel = new CompanyDetailsVM(_companyService.GetByID(id));
        //    return View(vmodel);
        //}

        //#region Delete
        //public ActionResult Delete(int id)
        //{
        //    CompanyDetailsVM vmodel = new CompanyDetailsVM(_companyService.GetByID(id));
        //    return View(vmodel);
        //}

        //[HttpPost]
        //public IActionResult Delete(CompanyDetailsVM vmodel)
        //{
        //    try
        //    {
        //        var model = _companyService.GetByID(vmodel.ID);
        //        var user = accountUtil.GetCurrentUser(User);
        //        if (_companyService.CompanyBelongsToUser(model, user.ID))
        //        {
        //            _companyService.DeletePermanently(model);
        //            return RedirectToAction("Index");
        //        }
        //        throw new Exception("Что то пошло не так. Компания не удалена");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Что то пошло не так. Компания не удалена");
        //    }
        //}
        //#endregion



    }
}