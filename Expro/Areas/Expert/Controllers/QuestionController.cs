using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    [Authorize(Policy = "ExpertOnly")]
    public class QuestionController : BaseController
    {
        private readonly IQuestionSearchService QuestionSearchService;
        private readonly IQuestionAdminActionsService QuestionAdminActionsService;
        private readonly IUserBalanceService UserBalanceService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IQuestionStatusService QuestionStatusService;
        private readonly IQuestionService QuestionService;
        private readonly IHangfireService HangfireService;

        public QuestionController(
            IQuestionService questionService,
            IQuestionSearchService questionSearchService,
            IHangfireService hangfireService,
            IQuestionAdminActionsService questionAdminActionsService,
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager,
            IQuestionStatusService questionStatusService)
        {
            QuestionAdminActionsService = questionAdminActionsService;
            UserBalanceService = userBalanceService;
            _userManager = userManager;
            QuestionSearchService = questionSearchService;
            QuestionStatusService = questionStatusService;
            QuestionService = questionService;
            HangfireService = hangfireService;
        }

        public IActionResult Index()
        {
            ViewData["statuses"] = QuestionStatusService.GetAsSelectList();
            return View();
        }

        [HttpPost]
        public IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, DocumentPriceTypesEnum? priceType = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);

            IQueryable<Question> dataIQueryable = QuestionSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType.Value,
                statusID,
                priceType,
                null,
                curUser.ID,
                null
            );

            dynamic data = dataIQueryable
                .Select(m => new QuestionListItemForExpertVM(m))
                .ToList();

            return Json(new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data,
                error = error
            });
        }
    }
}
