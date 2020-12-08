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

namespace Expro.Controllers
{
    public class CompaniesController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyService _companyService;
        private readonly ICountryService _countryService;
        private readonly ILawAreaService _lawAreaService;
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;

        public CompaniesController(
              UserManager<ApplicationUser> userManager,
              ICompanyService companyService,
              ICountryService countryService,
              ILawAreaService lawAreaService,
              IRegionService regionService,
              ICityService cityService
              )
        {
            _userManager = userManager;
            _companyService = companyService;
            _countryService = countryService;
            _lawAreaService = lawAreaService;
            _regionService = regionService;
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            var user = accountUtil.GetCurrentUser(User);
            ViewData["companyListVM"] = new CompanyDetailsVM().GetListOfCompanyDetailsVM(_companyService.GetCompanyByCreatorID(user.ID));
            return View();
        }



        public IActionResult Create()
        {
            GetCompanyViewDataForCreate();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CompanyEditVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);
            GetCompanyViewDataForCreate();
            if (ModelState.IsValid && user != null)
            {
                Company model = new Company();
                model.CompanyStatusID = (int)CompanyStatusEnum.WaitingForApproval;
                PropertyCopier.CopyTo(vmodel, model);

                _lawAreaService.UpdateCompanyLawAreas(model, vmodel.LawAreas);
                _companyService.Add(model, user.ID);

                return RedirectToAction("Index");
            }
            return View(vmodel);
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


        public ActionResult Edit(int id)
        {
            var user = accountUtil.GetCurrentUser(User);
            if (!_companyService.CompanyBelongsToUser(id, user.ID))
            {
                throw new Exception("Редактирование невозможно");
            }

            CompanyEditVM vmodel = new CompanyEditVM(_companyService.GetByID(id));
            GetCompanyViewDataForEdit(vmodel);
            return View(vmodel);
        }

        [HttpPost]
        public ActionResult Edit(CompanyEditVM vmodel)
        {
            var user = accountUtil.GetCurrentUser(User);

            if (!_companyService.CompanyBelongsToUser(vmodel.ID, user.ID))
            {
                throw new Exception("Редактирование невозможно");
            }
            
            GetCompanyViewDataForEdit(vmodel);
            
            if (ModelState.IsValid && user != null)
            {
                Company model = _companyService.GetActiveByID(vmodel.ID);
                model.CompanyStatusID = (int)CompanyStatusEnum.WaitingForApproval;
                PropertyCopier.CopyTo(vmodel, model);
                _lawAreaService.UpdateCompanyLawAreas(model, vmodel.LawAreas);                
                _companyService.Update(model);

                return RedirectToAction("Index");
            }
            return View(vmodel);
        }

        public void GetCompanyViewDataForEdit(CompanyEditVM vmodel)
        {
            ViewData["lawAreas"] = _lawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = vmodel.LawAreas.Contains(m.ID),
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            ViewData["regions"] = _regionService.GetAsSelectListOne(vmodel.RegionID);
            ViewData["cities"] = _cityService.GetAsSelectListOne(vmodel.CityID);
        }



        public ActionResult Details(int id)
        {
            CompanyDetailsVM vmodel = new CompanyDetailsVM(_companyService.GetByID(id));
            return View(vmodel);
        }

        #region Delete
        public ActionResult Delete(int id)
        {
            CompanyDetailsVM vmodel = new CompanyDetailsVM(_companyService.GetByID(id));
            return View(vmodel);
        }

        [HttpPost]
        public IActionResult Delete(CompanyDetailsVM vmodel)
        {
            try
            {
                var model = _companyService.GetByID(vmodel.ID);
                var user = accountUtil.GetCurrentUser(User);
                if (_companyService.CompanyBelongsToUser(model, user.ID))
                {
                    _companyService.DeletePermanently(model);
                    return RedirectToAction("Index");
                }
                throw new Exception("Что то пошло не так. Компания не удалена");
            }
            catch (Exception ex)
            {
                throw new Exception("Что то пошло не так. Компания не удалена");
            }
        }
        #endregion


        //private void PrepareViewData()
        //{
        //    ViewData["lawAreas"] = _lawAreaService.GetAsIQueryable()
        //        .Select(m => new SelectListItemWithParent()
        //        {
        //            Value = m.ID.ToString(),
        //            Text = m.Name,
        //            ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
        //        }).ToList();
        //}

        //edit
        //private void PrepareViewData(List<int> selectedLawAreaIDs)
        //{
        //    ViewData["lawAreas"] = _lawAreaService.GetAsIQueryable()
        //        .Select(m => new SelectListItemWithParent()
        //        {
        //            Value = m.ID.ToString(),
        //            Text = m.Name,
        //            Selected = selectedLawAreaIDs.Contains(m.ID),
        //            ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
        //        }).ToList();
        //}
    }
}