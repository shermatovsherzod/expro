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
    public class BaseVacancyController : BaseController
    {
        private readonly IVacancySearchService _vacancySearchService;
        private readonly IVacancyService _vacancyService;
        private readonly IVacancyAdminActionsService _vacancyAdminActionsService;
        public BaseVacancyController(
            IVacancySearchService vacancySearchService,
            IVacancyService vacancyService,
            IVacancyAdminActionsService vacancyAdminActionsService
            )
        {
            _vacancySearchService = vacancySearchService;
            _vacancyService = vacancyService;
            _vacancyAdminActionsService = vacancyAdminActionsService;
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

            IQueryable<Vacancy> dataIQueryable = _vacancySearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType,
                statusID,
                ""
            );

            dynamic data = new VacancyDetailsVM().GetListOfVacancyDetailsVM(dataIQueryable);

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
            var vacancy = _vacancyService.GetByID(id);
            if (vacancy == null)
                throw new Exception("Вакансия не найдена");

            VacancyDetailsVM vacancyDetailsVM = new VacancyDetailsVM(vacancy);

            return View(vacancyDetailsVM);
        }

        [HttpPost]
        public virtual IActionResult Approve(int id)
        {
            try
            {
                var vacancy = _vacancyService.GetByID(id);
                if (vacancy == null)
                    throw new Exception("Вакансия не найдена");

                var curUser = accountUtil.GetCurrentUser(User);
                _vacancyAdminActionsService.Approve(vacancy, curUser.ID);
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
                var vacancy = _vacancyService.GetByID(id);
                if (vacancy == null)
                    throw new Exception("Вакансия не найдена");

                var curUser = accountUtil.GetCurrentUser(User);
                _vacancyAdminActionsService.Reject(vacancy, curUser.ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}