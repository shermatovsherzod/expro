using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanySearchService _companySearchService;
        private readonly ICompanyService _companyService;
        private readonly ILawAreaService _lawAreaService;
        private readonly IRegionService _regionService;

        public CompanyController(
             ICompanySearchService companySearchService,
             ICompanyService companyService,
             ILawAreaService lawAreaService,
             IRegionService regionService)
        {
            _companySearchService = companySearchService;
            _companyService = companyService;
            _lawAreaService = lawAreaService;
            _regionService = regionService;
        }

        public IActionResult Index()
        {
            ViewData["lawAreas"] = _lawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = false,
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            ViewData["regions"] = _regionService.GetAsSelectList();

            return View();
        }

        [HttpPost]
        public IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? lawAreaParent = null,
            int[] lawAreas = null,
            int? regionID = null,
            int? cityID = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);

            IQueryable<Company> dataIQueryable = _companySearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                null,
                null,
                null,
                lawAreaParent,
                lawAreas,
                regionID,
                cityID
            );

            dynamic data = new CompanyDetailsVM().GetListOfCompanyDetailsVM(dataIQueryable);

            return Json(new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data,
                error = error
            });
        }

        public IActionResult Details(int id)
        {
            var company = _companyService.GetByID(id);
            if (company == null)
                throw new Exception("Компания не найдена");

            CompanyDetailsVM companyDetailsVM = new CompanyDetailsVM(company);

            return View(companyDetailsVM);
        }
    }
}
