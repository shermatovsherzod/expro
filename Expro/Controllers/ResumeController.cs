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
    public class ResumeController : BaseController
    {  
        private readonly IResumeStatusService _resumeStatusService;
        private readonly IResumeSearchService _resumeSearchService;
        private readonly IResumeService _resumeService;
        private readonly IResumeAdminActionsService _resumeAdminActionsService;

        public ResumeController(
             IResumeSearchService resumeSearchService,
             IResumeService resumeService,
             IResumeStatusService resumeStatusService,
             IResumeAdminActionsService resumeAdminActionsService)
        {
            _resumeStatusService = resumeStatusService;
            _resumeSearchService = resumeSearchService;
            _resumeService = resumeService;
            _resumeAdminActionsService = resumeAdminActionsService;
        }

        public IActionResult Index()
        {
            ViewData["statuses"] = _resumeStatusService.GetAsSelectList();
            return View();
        }

        [HttpPost]
        public IActionResult Search(int draw, int? start = null, int? length = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            IQueryable<Resume> dataIQueryable = _resumeSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                null,
                null,
                null
            );

            dynamic data = new ResumeDetailsVM().GetListOfResumeDetailsVM(dataIQueryable);

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
            var resume = _resumeService.GetByID(id);
            if (resume == null)
                throw new Exception("Вакансия не найдена");

            ResumeDetailsVM resumeDetailsVM = new ResumeDetailsVM(resume);

            return View(resumeDetailsVM);
        }
    }
}
