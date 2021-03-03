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
    public class BaseResumeController : BaseController
    {
        private readonly IResumeSearchService _resumeSearchService;
        private readonly IResumeService _resumeService;
        private readonly IResumeAdminActionsService _resumeAdminActionsService;
        public BaseResumeController(
            IResumeSearchService resumeSearchService,
            IResumeService resumeService,
            IResumeAdminActionsService resumeAdminActionsService
            )
        {
            _resumeSearchService = resumeSearchService;
            _resumeService = resumeService;
            _resumeAdminActionsService = resumeAdminActionsService;
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

            IQueryable<Resume> dataIQueryable = _resumeSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType,
                statusID,
                "",
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

        public virtual IActionResult Details(int id)
        {
            var resume = _resumeService.GetByID(id);
            if (resume == null)
                throw new Exception("Резюме не найдено");

            ResumeDetailsVM resumeDetailsVM = new ResumeDetailsVM(resume);

            return View(resumeDetailsVM);
        }

        [HttpPost]
        public virtual IActionResult Approve(int id)
        {
            try
            {
                var resume = _resumeService.GetByID(id);
                if (resume == null)
                    throw new Exception("Резюме не найдено");

                var curUser = accountUtil.GetCurrentUser(User);
                _resumeAdminActionsService.Approve(resume, curUser.ID);
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
                var resume = _resumeService.GetByID(id);
                if (resume == null)
                    throw new Exception("Резюме не найдено");

                var curUser = accountUtil.GetCurrentUser(User);
                _resumeAdminActionsService.Reject(resume, curUser.ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}