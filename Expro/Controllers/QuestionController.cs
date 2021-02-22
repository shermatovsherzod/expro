using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Expro.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly IQuestionAnswerService QuestionAnswerService;
        private readonly IQuestionService QuestionService;
        private readonly IHangfireService HangfireService;
        private readonly ILawAreaService LawAreaService;
        private readonly IQuestionSearchService QuestionSearchService;
        private readonly IQuestionCounterService QuestionCounterService;
        private readonly IUserBalanceService UserBalanceService;
        private readonly IUserService _userService;
        private readonly IUserRatingService _userRatingService;

        public QuestionController(
            IQuestionService questionService,
            IQuestionSearchService questionSearchService,
            IUserBalanceService userBalanceService,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService,
            IQuestionCounterService questionCounterService,
            IQuestionAnswerService questionAnswerService,
            IHangfireService hangfireService,
            IUserService userService,
            IUserRatingService userRatingService,
            IStringLocalizer<Resources.ResourceTexts> localizer)
        {
            QuestionService = questionService;
            QuestionAnswerService = questionAnswerService;
            HangfireService = hangfireService;
            LawAreaService = lawAreaService;
            QuestionSearchService = questionSearchService;
            QuestionCounterService = questionCounterService;
            UserBalanceService = userBalanceService;
            _userService = userService;
            _userRatingService = userRatingService;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["lawAreas"] = LawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = false,
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, 
            DocumentPriceTypesEnum? priceType = null,
            int[] lawAreas = null)
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

                null,
                (int)DocumentStatusesEnum.Approved,
                priceType,
                null,
                null,
                lawAreas
            );

            dynamic data = dataIQueryable
                .Select(m => new QuestionListItemForSiteVM(m))
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

        //success = "answerAddedSuccessfully/commentAddedSuccessfully/likedSuccessfully"
        public IActionResult Details(int id, string successMessage = null)
        {
            var curUser = accountUtil.GetCurrentUser(User);

            var question = QuestionService.GetApprovedWithAnswersAndCommentsByID(id);
            if (question == null)
                throw new Exception(_localizer["QuestionNotFound"]);

            if (string.IsNullOrWhiteSpace(successMessage))
                QuestionCounterService.IncrementNumberOfViews(question);
            else
                ViewData["successMessage"] = successMessage;

            QuestionDetailsForSiteVM documentVM = new QuestionDetailsForSiteVM(question, curUser.ID);

            bool curUserIsAllowedToAnswer = false;
            bool curUserIsAllowedToComment = false;
            bool curUserIsAllowedToDistributeFee = false;
            bool curUserIsAllowedToComplete = false;

            if (User.Identity.IsAuthenticated)
            {
                if (curUser.IsExpert)
                {
                    curUserIsAllowedToAnswer = true;
                    curUserIsAllowedToComment = true;

                    var appUser = _userService.GetByID(curUser.ID);
                    if (appUser == null)
                        throw new Exception(_localizer["UserNotFound"]);

                    if (!QuestionService.IsFree(question))
                    {
                        ViewData["userIsAllowedToWorkWithPaidMaterials"] =
                            _userService.UserIsAllowedToWorkWithPaidMaterials(appUser);
                    }
                }
                else if(QuestionService.BelongsToUser(question, curUser.ID))
                {
                    curUserIsAllowedToComment = true;
                    curUserIsAllowedToDistributeFee = true;
                }
                else if (curUser.IsAdmin)
                {
                    curUserIsAllowedToComplete = QuestionService.AdminIsAllowedToComplete(question);
                    curUserIsAllowedToDistributeFee = curUserIsAllowedToComplete;
                }
            }

            ViewData["curUserIsAllowedToAnswer"] = curUserIsAllowedToAnswer;
            ViewData["curUserIsAllowedToComment"] = curUserIsAllowedToComment;
            ViewData["curUserIsAllowedToDistributeFee"] = curUserIsAllowedToDistributeFee;
            ViewData["curUserIsAllowedToComplete"] = curUserIsAllowedToComplete;
            ViewData["curPageUrl"] = Request.Path.Value;

            return View(documentVM);
        }

        //ajax
        [Authorize(Policy = "ExpertOnly")]
        [HttpPost]
        public IActionResult AddAnswer(/*[FromBody]*/ QuestionAnswerCreateVM answerCreateVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);
                    var question = QuestionService.GetApprovedByID(answerCreateVM.QuestionID);
                    if (question == null)
                        throw new Exception(_localizer["QuestionNotFound"]);

                    if (QuestionService.IsCompleted(question))
                        throw new Exception(_localizer["QuestionClosed"]);

                    QuestionAnswer answer = answerCreateVM.ToModel();
                    
                    var curUserFromDB = _userService.GetByID(curUser.ID);
                    if (curUserFromDB == null)
                        throw new Exception(_localizer["UserNotFound"]);

                    int points = Constants.PointsFor.QUESTION_ANSWER;
                    _userRatingService.AddPointsToUser(points, curUserFromDB);

                    QuestionAnswerService.Add(answer, curUser.ID);

                    return Ok(new { id = answer.ID });
                }
                else
                    //throw new Exception("Неверные данные");
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        //ajax
        [Authorize(Policy = "AdminOrSimpleUserOnly")]
        [HttpPost]
        public IActionResult SaveDistribution([FromBody] QuestionFeeDistributionVM distributedAnswers)
        {
            try
            {
                var question = QuestionService.GetByID(distributedAnswers.QuestionID);
                if (question == null)
                    throw new Exception(_localizer["QuestionNotFound"]);

                if (QuestionService.IsCompleted(question))
                    throw new Exception(_localizer["QuestionClosed"]);

                if (QuestionService.IsFree(question))
                    throw new Exception(_localizer["QuestionHasNoFee"]);

                if (distributedAnswers == null 
                    || distributedAnswers.Answers == null
                    || distributedAnswers.Answers.Count == 0)
                {
                    throw new Exception(_localizer["SelectAtLeastOneAnswer"]);
                }

                var percentages = distributedAnswers.Answers
                    .Select(m => m.Percentage)
                    .ToList();
                if (!QuestionAnswerService.DistributionIsCorrect(percentages))
                    throw new Exception(_localizer["ItMustBe100PercentInTotal"]);

                foreach (var item in distributedAnswers.Answers)
                {
                    var answer = QuestionAnswerService.GetByID(item.AnswerID);
                    if (answer == null)
                        throw new Exception(_localizer["SomethingIsWrongWithSelectedAnswers"]);

                    answer.PaidFee = QuestionAnswerService.CalculatePaidFee(question.Price.Value, item.Percentage);

                    UserBalanceService.ReplenishBalance(answer.Creator, answer.PaidFee.Value);
                }

                var curUser = accountUtil.GetCurrentUser(User);
                QuestionService.CompleteWithDistribution(question, curUser.ID);
                HangfireService.CancelJob(question.QuestionCompletionJobID);

                return Ok(new { successMessage = _localizer["FeeIsSuccessfullyDistributed"] });
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        //ajax
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public IActionResult Complete(int id)
        {
            try
            {
                var question = QuestionService.GetByID(id);
                if (question == null)
                    throw new Exception(_localizer["QuestionNotFound"]);

                if (QuestionService.IsCompleted(question))
                    throw new Exception(_localizer["QuestionClosed"]);

                if (QuestionService.IsFree(question))
                    throw new Exception(_localizer["QuestionHasNoFee"]);

                UserBalanceService.ReplenishBalance(question.Creator, question.Price.Value);

                var curUser = accountUtil.GetCurrentUser(User);
                QuestionService.Complete(question, curUser.ID);
                HangfireService.CancelJob(question.QuestionCompletionJobID);

                return Ok(new { successMessage = _localizer["FeeIsSuccessfullyDistributed"] });
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}
