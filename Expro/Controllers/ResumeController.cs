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
        private readonly IRegionService _regionService;

        public ResumeController(
             IResumeSearchService resumeSearchService,
             IResumeService resumeService,
             IResumeStatusService resumeStatusService,
             IResumeAdminActionsService resumeAdminActionsService,
             IRegionService regionService)
        {
            _resumeStatusService = resumeStatusService;
            _resumeSearchService = resumeSearchService;
            _resumeService = resumeService;
            _resumeAdminActionsService = resumeAdminActionsService;
            _regionService = regionService;
        }

        public IActionResult Index()
        {
            //ViewData["statuses"] = _resumeStatusService.GetAsSelectList();
            ViewData["regions"] = _regionService.GetAsSelectList();

            bool showAddNewItemButton = false;

            if (User.Identity.IsAuthenticated)
            {
                var curUser = accountUtil.GetCurrentUser(User);
                ViewData["curUserType"] = curUser.UserType?.ToString();

                if (curUser.IsSimpleUser)
                {
                    showAddNewItemButton = true;
                    ViewData["curUserArea"] = UserAreasEnum.User.ToString();
                }
                else if (curUser.IsExpert)
                {
                    showAddNewItemButton = true;
                    ViewData["curUserArea"] = UserAreasEnum.Expert.ToString();
                }
            }
            ViewData["showAddNewItemButton"] = showAddNewItemButton;

            return View();
        }

        [HttpPost]
        public IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? regionID = null,
            int? cityID = null)
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
                null,
                regionID,
                cityID
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
