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

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : BaseController
    {  
        private readonly ICompanyStatusService _companyStatusService;
        private readonly ICompanySearchService _companySearchService;
        private readonly ICompanyService _companyService;
        private readonly ICompanyAdminActionsService _companyAdminActionsService;
        private readonly ILawAreaService _lawAreaService;

        public CompanyController(
             ICompanySearchService companySearchService,
             ICompanyService companyService,
             ICompanyStatusService companyStatusService,
             ICompanyAdminActionsService companyAdminActionsService,
             ILawAreaService lawAreaService)
        {
            _companyStatusService = companyStatusService;
            _companySearchService = companySearchService;
            _companyService = companyService;
            _companyAdminActionsService = companyAdminActionsService;
            _lawAreaService = lawAreaService;
        }

        public IActionResult Index()
        {
            ViewData["statuses"] = _companyStatusService.GetAsSelectList();

            ViewData["lawAreas"] = _lawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = false,
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Search(int draw, int? start = null, int? length = null,  
            int? statusID = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            IQueryable<Company> dataIQueryable = _companySearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                UserTypesEnum.Admin,
                statusID,
                null,
                null,
                null,
                null,
                null);

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

        [HttpPost]
        public IActionResult Approve(int id)
        {
            try
            {
                var company = _companyService.GetByID(id);
                if (company == null)
                    throw new Exception("Компания не найдена");

                var curUser = accountUtil.GetCurrentUser(User);
                _companyAdminActionsService.Approve(company, curUser.ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            try
            {
                var company = _companyService.GetByID(id);
                if (company == null)
                    throw new Exception("Компания не найдена");

                var curUser = accountUtil.GetCurrentUser(User);
                _companyAdminActionsService.Reject(company, curUser.ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}
