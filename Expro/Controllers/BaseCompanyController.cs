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
    public class BaseCompanyController : BaseController
    {
        private readonly ICompanySearchService _companySearchService;
        private readonly ICompanyService _companyService;
        private readonly ICompanyAdminActionsService _companyAdminActionsService;
        public BaseCompanyController(
            ICompanySearchService companySearchService,
            ICompanyService companyService,
            ICompanyAdminActionsService companyAdminActionsService
            )
        {
            _companySearchService = companySearchService;
            _companyService = companyService;
            _companyAdminActionsService = companyAdminActionsService;
        }

        public virtual IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual IActionResult Search(
           int draw, int? start = null, int? length = null, int? statusID = null)
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

                curUser.UserType,
                statusID,
                ""
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

        public virtual IActionResult Details(int id)
        {
            var company = _companyService.GetByID(id);
            if (company == null)
                throw new Exception("Вакансия не найдена");

            CompanyDetailsVM companyDetailsVM = new CompanyDetailsVM(company);

            return View(companyDetailsVM);
        }

        [HttpPost]
        public virtual IActionResult Approve(int id)
        {
            try
            {
                var company = _companyService.GetByID(id);
                if (company == null)
                    throw new Exception("Вакансия не найдена");

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
        public virtual IActionResult Reject(int id)
        {
            try
            {
                var company = _companyService.GetByID(id);
                if (company == null)
                    throw new Exception("Вакансия не найдена");

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