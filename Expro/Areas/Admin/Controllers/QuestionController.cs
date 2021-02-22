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
using Microsoft.Extensions.Localization;

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
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
            IQuestionStatusService questionStatusService,
            IStringLocalizer<Resources.ResourceTexts> localizer)
        {
            QuestionAdminActionsService = questionAdminActionsService;
            UserBalanceService = userBalanceService;
            _userManager = userManager;
            QuestionSearchService = questionSearchService;
            QuestionStatusService = questionStatusService;
            QuestionService = questionService;
            HangfireService = hangfireService;
            _localizer = localizer;
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
                null,
                null
            );

            dynamic data = dataIQueryable
                .Select(m => new QuestionListItemForAdminVM(m))
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

        public IActionResult Details(int id)
        {
            var question = QuestionService.GetByID(id);
            if (question == null)
                throw new Exception(_localizer["QuestionNotFound"]);

            QuestionDetailsForAdminVM documentVM = new QuestionDetailsForAdminVM(question);

            return View(documentVM);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            try
            {
                var question = QuestionService.GetByID(id);
                if (question == null)
                    throw new Exception(_localizer["QuestionNotFound"]);

                var curUser = accountUtil.GetCurrentUser(User);

                if (!QuestionAdminActionsService.ApprovingIsAllowed(question))
                    throw new Exception(_localizer["StatusDoesNotAllowToApprove"]);

                QuestionAdminActionsService.Approve(question, curUser.ID);
                if (question.PriceType == DocumentPriceTypesEnum.Paid)
                {
                    question.QuestionCompletionJobID = HangfireService.CreateJobForQuestionCompletionDeadline(question);
                    QuestionService.Update(question);
                }

                //cancel hangfire jobs
                HangfireService.CancelJob(question.RejectionJobID);

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
                var document = QuestionService.GetByID(id);
                if (document == null)
                    throw new Exception(_localizer["QuestionNotFound"]);

                var curUser = accountUtil.GetCurrentUser(User);

                if (!QuestionAdminActionsService.RejectingIsAllowed(document))
                    throw new Exception(_localizer["StatusDoesNotAllowToReject"]);

                if (document.PriceType == DocumentPriceTypesEnum.Paid)
                {
                    UserBalanceService.ReplenishBalance(document.Creator, document.Price.Value);
                }

                QuestionAdminActionsService.Reject(document, curUser.ID);
                
                HangfireService.CancelJob(document.RejectionJobID);

                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}
