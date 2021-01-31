//using System;
//using System.Collections.Generic;
//using Expro.Controllers;
//using Expro.Models;
//using Expro.Models.Enums;
//using Expro.Services.Interfaces;
//using Expro.Utils;
//using Expro.ViewModels;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;

//namespace Expro.Controllers
//{
//    public class BaseVacancyController : BaseController
//    {
//        private readonly IVacancySearchService _vacancySearchService;
//        private readonly IVacancyService _vacancyService;
//        private readonly IVacancyAdminActionsService _vacancyAdminActionsService;
//        public BaseVacancyController(
//            IVacancySearchService vacancySearchService,
//            IVacancyService vacancyService,
//            IVacancyAdminActionsService vacancyAdminActionsService
//            )
//        {
//            _vacancySearchService = vacancySearchService;
//            _vacancyService = vacancyService;
//            _vacancyAdminActionsService = vacancyAdminActionsService;
//        }

//        public virtual IActionResult Index()
//        {
//            return View();
//        }

//        [HttpPost]
//        public virtual IActionResult Search(
//           int draw, int? start = null, int? length = null, int? statusID = null)
//        {
            
//        }

//        public virtual IActionResult Details(int id)
//        {
            
//        }

//        [HttpPost]
//        public virtual IActionResult Approve(int id)
//        {
            
//        }

//        [HttpPost]
//        public virtual IActionResult Reject(int id)
//        {
            
//        }
//    }
//}