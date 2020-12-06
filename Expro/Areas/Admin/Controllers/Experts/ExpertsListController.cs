﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers.Experts
{
    [Area("Admin")]
    public class ExpertsListController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWorkExperienceService _workExperienceService;
        private readonly IExpertEducationService _expertEducationService;
        private readonly IUserStatusService _userStatusService;    
        
        public ExpertsListController(
              UserManager<ApplicationUser> userManager,
              IWorkExperienceService workExperienceService,
              IUserStatusService userStatusService,
              IExpertEducationService expertEducationService
            )
        {
            _userManager = userManager;
            _workExperienceService = workExperienceService;
            _userStatusService = userStatusService;
            _expertEducationService= expertEducationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetExperts()
        {
            var expertsList = _userManager.Users.Where(c => c.UserType == UserTypesEnum.Expert);

            var draw1 = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal1 = expertsList.Count();

            // Sorting
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                expertsList = expertsList.OrderBy(sortColumn + " " + sortColumnDirection);
            }

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                expertsList = expertsList.Where(g => g.FirstName.ToLower().Contains(searchValue.ToLower()) || g.LastName.ToLower().Contains(searchValue.ToLower()));
            }
            List<ProfileExpertApproveInfoVM> expertsListVM = new List<ProfileExpertApproveInfoVM>();

            foreach (var item in expertsList)
            {
                expertsListVM.Add(new ProfileExpertApproveInfoVM(item));
            }

            int filterTotal = expertsListVM.Count();
            var data1 = expertsListVM.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw1, recordsTotal = recordsTotal1, recordsFiltered = filterTotal, data = data1 });
        }

        public IActionResult ExpertDetails(string id)
        {
            var expert = _userManager.Users.FirstOrDefault(c => c.Id == id);
            if (expert != null)
            {
                ProfileExpertFullInfoVM vmodel = new ProfileExpertFullInfoVM(expert);

                ViewData["expertEducationListVM"] = new ExpertProfileEducationDetailsVM().GetListOfExpertProfileEducationDetailsVM(_expertEducationService.GetExpertEducationsByCreatorID(expert.Id));
              
                ViewData["workExperienceEducationListVM"] = new ExpertProfileWorkExperienceDetailsVM().GetListOfExpertProfileWorkExperienceDetailsVM(_workExperienceService.GetExpertWorkExperienceByCreatorID(expert.Id));
                return View(vmodel);
            }
            throw new Exception("Эксперт не найден");
        }

        [HttpPost]
        public async Task<ActionResult> Approve(string id)
        {
            if (await _userStatusService.Approve(id))
                return Json(new { success = true });
            throw new Exception("Пользователь не подтвержден");
        }

        [HttpPost]
        public async Task<ActionResult> Reject(string id)
        {
            if (await _userStatusService.Reject(id))
                return Json(new { success = true });
            throw new Exception("Пользователю не отказано");
        }

    }

}