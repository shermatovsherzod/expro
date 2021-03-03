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
    public class VacancyController : BaseController
    {  
        private readonly IVacancyStatusService _vacancyStatusService;
        private readonly IVacancySearchService _vacancySearchService;
        private readonly IVacancyService _vacancyService;
        private readonly IVacancyAdminActionsService _vacancyAdminActionsService;

        public VacancyController(
             IVacancySearchService vacancySearchService,
             IVacancyService vacancyService,
             IVacancyStatusService vacancyStatusService,
             IVacancyAdminActionsService vacancyAdminActionsService)
        {
            _vacancyStatusService = vacancyStatusService;
            _vacancySearchService = vacancySearchService;
            _vacancyService = vacancyService;
            _vacancyAdminActionsService = vacancyAdminActionsService;
        }

        public IActionResult Index()
        {
            ViewData["statuses"] = _vacancyStatusService.GetAsSelectList();
            return View();
        }

        [HttpPost]
        public IActionResult Search(int draw, int? start = null, int? length = null,  int? statusID = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            IQueryable<Vacancy> dataIQueryable = _vacancySearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                UserTypesEnum.Admin,
                statusID,
                null,
                null,
                null
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


        public IActionResult Details(int id)
        {
            var vacancy = _vacancyService.GetByID(id);
            if (vacancy == null)
                throw new Exception("Вакансия не найдена");

            VacancyDetailsVM vacancyDetailsVM = new VacancyDetailsVM(vacancy);

            return View(vacancyDetailsVM);
        }

        [HttpPost]
        public IActionResult Approve(int id)
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
        public IActionResult Reject(int id)
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
