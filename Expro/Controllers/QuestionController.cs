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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public QuestionController(
            IQuestionService questionService,
            IQuestionSearchService questionSearchService,
            IUserBalanceService userBalanceService,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService,
            IQuestionCounterService questionCounterService,
            IQuestionAnswerService questionAnswerService,
            IHangfireService hangfireService)
        {
            QuestionService = questionService;
            QuestionAnswerService = questionAnswerService;
            HangfireService = hangfireService;
            LawAreaService = lawAreaService;
            QuestionSearchService = questionSearchService;
            QuestionCounterService = questionCounterService;
            UserBalanceService = userBalanceService;
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
            //ApplicationUser user = await _userManager.GetUserAsync(User);

            IQueryable<Question> dataIQueryable = QuestionSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                null,
                statusID,
                priceType,
                curUser.ID,
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

        public IActionResult Details(int id)
        {
            var question = QuestionService.GetApprovedWithAnswersAndCommentsByID(id);
            if (question == null)
                throw new Exception("Вопрос не найден");

            QuestionCounterService.IncrementNumberOfViews(question);

            QuestionDetailsForSiteVM documentVM = new QuestionDetailsForSiteVM(question);

            bool curUserIsAllowedToAnswer = false;
            bool curUserIsAllowedToComment = false;
            bool curUserIsAllowedToDistributeFee = false;
            bool curUserIsAllowedToComplete = false;

            if (User.Identity.IsAuthenticated)
            {
                var curUser = accountUtil.GetCurrentUser(User);
                if (curUser.IsExpert)
                {
                    curUserIsAllowedToAnswer = true;
                    curUserIsAllowedToComment = true;
                }
                else if(QuestionService.BelongsToUser(question, curUser.ID))
                {
                    curUserIsAllowedToComment = true;
                    curUserIsAllowedToDistributeFee = true;
                }
                else if (curUser.IsAdmin)
                {
                    curUserIsAllowedToComplete = QuestionService.AdminIsAllowedToComplete(question);
                    curUserIsAllowedToDistributeFee = true;
                }
            }

            ViewData["curUserIsAllowedToAnswer"] = curUserIsAllowedToAnswer;
            ViewData["curUserIsAllowedToComment"] = curUserIsAllowedToComment;
            ViewData["curUserIsAllowedToDistributeFee"] = curUserIsAllowedToDistributeFee;
            ViewData["curUserIsAllowedToComplete"] = curUserIsAllowedToComplete;

            return View(documentVM);
        }

        //ajax
        //expertPolicy
        [HttpPost]
        public IActionResult AddAnswer(/*[FromBody]*/ QuestionAnswerCreateVM answerCreateVM)
        {
            try
            {
                var curUser = accountUtil.GetCurrentUser(User);
                var document = QuestionService.GetApprovedByID(answerCreateVM.QuestionID);
                if (document == null)
                    throw new Exception("Вопрос не найден");

                if (QuestionService.IsCompleted(document))
                    throw new Exception("Вопрос уже завершен");

                QuestionAnswer answer = answerCreateVM.ToModel();
                QuestionAnswerService.Add(answer, curUser.ID);

                return Ok(new { id = answer.ID });
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        //ajax
        [HttpPost]
        public IActionResult SaveDistribution([FromBody] QuestionFeeDistributionVM distributedAnswers)
        {
            try
            {
                var question = QuestionService.GetByID(distributedAnswers.QuestionID);
                if (question == null)
                    throw new Exception("Вопрос не найден");

                if (QuestionService.IsCompleted(question))
                    throw new Exception("Вопрос уже завершен");

                if (QuestionService.IsFree(question))
                    throw new Exception("Вопрос не имеет гонорара");

                if (distributedAnswers == null 
                    || distributedAnswers.Answers == null
                    || distributedAnswers.Answers.Count == 0)
                {
                    throw new Exception("Выберите хотя бы один ответ");
                }

                var percentages = distributedAnswers.Answers
                    .Select(m => m.Percentage)
                    .ToList();
                if (!QuestionAnswerService.DistributionIsCorrect(percentages))
                    throw new Exception("Суммарно должно быть 100%");

                foreach (var item in distributedAnswers.Answers)
                {
                    var answer = QuestionAnswerService.GetByID(item.AnswerID);
                    if (answer == null)
                        throw new Exception("Что-то не то с выбранными ответами");

                    answer.PaidFee = QuestionAnswerService.CalculatePaidFee(question.Price.Value, item.Percentage);

                    UserBalanceService.ReplenishBalance(answer.Creator, answer.PaidFee.Value);
                }

                var curUser = accountUtil.GetCurrentUser(User);
                QuestionService.CompleteWithDistribution(question, curUser.ID);
                HangfireService.CancelJob(question.QuestionCompletionJobID);

                return Ok(new { successMessage = "Гонорар успешно распределен" });
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        //ajax
        //adminPolicy
        [HttpPost]
        public IActionResult Complete(int id)
        {
            try
            {
                var question = QuestionService.GetByID(id);
                if (question == null)
                    throw new Exception("Вопрос не найден");

                if (QuestionService.IsCompleted(question))
                    throw new Exception("Вопрос уже завершен");

                if (QuestionService.IsFree(question))
                    throw new Exception("Вопрос не имеет гонорара");

                UserBalanceService.ReplenishBalance(question.Creator, question.Price.Value);

                var curUser = accountUtil.GetCurrentUser(User);
                QuestionService.Complete(question, curUser.ID);
                HangfireService.CancelJob(question.QuestionCompletionJobID);

                return Ok(new { successMessage = "Гонорар успешно распределен" });
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}
