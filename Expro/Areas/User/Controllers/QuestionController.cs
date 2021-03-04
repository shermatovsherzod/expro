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

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Policy = "SimpleUserOnly")]
    public class QuestionController : BaseController
    {
        private readonly IQuestionSearchService QuestionSearchService;
        private readonly ILawAreaService LawAreaService;
        private readonly ILanguageService LanguageService;
        private readonly IAttachmentService AttachmentService;
        private readonly IQuestionService QuestionService;
        private readonly IHangfireService HangfireService;
        private readonly IQuestionStatusService QuestionStatusService;
        private readonly IUserBalanceService UserBalanceService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public QuestionController(
            IQuestionSearchService questionSearchService,
            ILawAreaService lawAreaService,
            ILanguageService languageService,
            IAttachmentService attachmentService,
            IQuestionService questionService,
            IHangfireService hangfireService,
            IQuestionStatusService questionStatusService,
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager,
            IUserService userService)
        {
            QuestionSearchService = questionSearchService;
            LawAreaService = lawAreaService;
            LanguageService = languageService;
            AttachmentService = attachmentService;
            QuestionService = questionService;
            HangfireService = hangfireService;
            QuestionStatusService = questionStatusService;
            UserBalanceService = userBalanceService;
            _userManager = userManager;
            _userService = userService;
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
                curUser.ID,
                null,
                null,
                null
            );

            dynamic data = dataIQueryable
                .Select(m => new QuestionListItemForUserVM(m))
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

        public IActionResult CreateFree()
        {
            PrepareViewData(null);

            return View();
        }

        [HttpPost]
        public IActionResult CreateFree(QuestionFreeEditVM modelVM)
        {
            return CreateSharedPost(modelVM, DocumentPriceTypesEnum.Free);
        }

        public IActionResult CreatePaid()
        {
            PrepareViewData(null);

            return View();
        }

        [HttpPost]
        public IActionResult CreatePaid(QuestionPaidEditVM modelVM)
        {
            return CreateSharedPost(modelVM, DocumentPriceTypesEnum.Paid);
        }

        private IActionResult CreateSharedPost(QuestionFreeEditVM modelVM, DocumentPriceTypesEnum priceType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);

                    var model = modelVM.ToModel();
                    LawAreaService.UpdateQuestionLawAreas(model, modelVM.LawAreas);
                    QuestionService.Add(model, curUser.ID);

                    if (modelVM.ActionType == DocumentActionTypesEnum.publish)
                    {
                        if (priceType == DocumentPriceTypesEnum.Paid)
                        {
                            ApplicationUser curAppUser = _userService.GetByID(curUser.ID);
                            int curUserBalance = UserBalanceService.GetBalance(curAppUser);
                            if (curUserBalance < model.Price)
                                throw new Exception("На Вашем балансе недостаточно средств");

                            UserBalanceService.TakeOffBalance(curAppUser, model.Price.Value);
                        }

                        QuestionService.Publish(model, curUser.ID);

                        modelVM.StatusID = (int)DocumentStatusesEnum.Approved;
                    }
                    else
                        QuestionService.Update(model, curUser.ID);

                    return Ok(new { id = model.ID });
                }
                else
                    throw new Exception("Неверные данные");
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
                //ModelState.AddModelError("", "Что-то пошло не так: " + ex.Message);
            }
        }

        public IActionResult EditFree(int id, bool? successfullyCreated = null)
        {
            var model = QuestionService.GetFreeByID(id);
            if (model == null)
                throw new Exception("Вопрос не найден");

            var modelVM = new QuestionFreeEditVM(model);

            var selectedLawAreaIDs = model.QuestionLawAreas.Select(m => m.LawAreaID).ToList();
            PrepareViewData(selectedLawAreaIDs);

            if (successfullyCreated.HasValue)
                ViewData["successfullySaved"] = successfullyCreated.Value;

            return View(modelVM);
        }

        [HttpPost]
        public IActionResult EditFree(QuestionFreeEditVM modelVM)
        {
            return EditSharedPost(modelVM, DocumentPriceTypesEnum.Free);
        }


        public IActionResult EditPaid(int id, bool? successfullyCreated = null)
        {
            var model = QuestionService.GetPaidByID(id);
            if (model == null)
                throw new Exception("Вопрос не найден");

            var modelVM = new QuestionPaidEditVM(model);

            var selectedLawAreaIDs = model.QuestionLawAreas.Select(m => m.LawAreaID).ToList();
            PrepareViewData(selectedLawAreaIDs);

            if (successfullyCreated.HasValue)
                ViewData["successfullySaved"] = successfullyCreated.Value;

            return View(modelVM);
        }

        [HttpPost]
        public IActionResult EditPaid(QuestionPaidEditVM modelVM)
        {
            return EditSharedPost(modelVM, DocumentPriceTypesEnum.Paid);
        }

        private IActionResult EditSharedPost(QuestionFreeEditVM modelVM, DocumentPriceTypesEnum priceType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Question modelFromDB;
                    if (priceType == DocumentPriceTypesEnum.Free)
                        modelFromDB = QuestionService.GetFreeByID(modelVM.ID);
                    else
                        modelFromDB = QuestionService.GetPaidByID(modelVM.ID);

                    if (modelFromDB == null)
                        throw new Exception("Вопрос не найден");

                    var curUser = accountUtil.GetCurrentUser(User);

                    if (!QuestionService.BelongsToUser(modelFromDB, curUser.ID))
                        throw new Exception("Данный вопрос вам не принадлежит");

                    if (!QuestionService.EditingIsAllowed(modelFromDB))
                        throw new Exception("Статус вопроса не позволяет отредактировать его");

                    var model = modelVM.ToModel(modelFromDB);
                    LawAreaService.UpdateQuestionLawAreas(model, modelVM.LawAreas);
                    modelVM.LawAreas = model.QuestionLawAreas.Select(m => m.LawAreaID).ToList();

                    if (modelVM.ActionType == DocumentActionTypesEnum.publish)
                    {
                        if (priceType == DocumentPriceTypesEnum.Paid)
                        {
                            ApplicationUser curAppUser = _userService.GetByID(curUser.ID);
                            int curUserBalance = UserBalanceService.GetBalance(curAppUser);
                            if (curUserBalance < model.Price)
                                throw new Exception("На Вашем балансе недостаточно средств");

                            UserBalanceService.TakeOffBalance(curAppUser, model.Price.Value);
                        }

                        QuestionService.Publish(model, curUser.ID);

                        modelVM.StatusID = (int)DocumentStatusesEnum.Approved;
                    }
                    else
                        QuestionService.Update(model, curUser.ID);



                    //if (modelVM.ActionType == DocumentActionTypesEnum.submitForApproval)
                    //{
                    //    ApplicationUser curAppUser = _userService.GetByID(curUser.ID);
                    //    int curUserBalance = UserBalanceService.GetBalance(curAppUser);
                    //    if (curUserBalance < model.Price)
                    //        throw new Exception("На Вашем балансе недостаточно средств");

                    //    QuestionService.SubmitForApproval(model, curUser.ID);

                    //    if (priceType == DocumentPriceTypesEnum.Paid)
                    //        UserBalanceService.TakeOffBalance(curAppUser, model.Price.Value);

                    //    model.RejectionJobID = HangfireService.CreateJobForQuestionRejectionDeadline(model);
                    //    QuestionService.Update(model);

                    //    modelVM.StatusID = (int)DocumentStatusesEnum.WaitingForApproval;
                    //}
                    //else
                    //    QuestionService.Update(model, curUser.ID);

                    ViewData["successfullySaved"] = true;
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные данные");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Что-то пошло не так: " + ex.Message);
            }

            var selectedLawAreaIDs = modelVM.LawAreas;

            PrepareViewData(selectedLawAreaIDs);

            if (modelVM.AttachmentID.HasValue)
            {
                var attachment = AttachmentService.GetActiveByID(modelVM.AttachmentID.Value);
                if (attachment != null)
                    modelVM.Attachment = new AttachmentDetailsVM(attachment);
            }

            return View(modelVM);
        }

        //ajax
        [HttpPost]
        public IActionResult SetAsPaid(QuestionSetAsPaidFormVM setAsPaidFormVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Что-то не то");

                var curUser = accountUtil.GetCurrentUser(User);
                ApplicationUser curAppUser = _userService.GetByID(curUser.ID);
                int curUserBalance = UserBalanceService.GetBalance(curAppUser);
                if (curUserBalance < setAsPaidFormVM.Price)
                    throw new Exception("На Вашем балансе недостаточно средств");

                Question question = QuestionService.GetFreeByID(setAsPaidFormVM.ID);
                if (question == null)
                    throw new Exception("Вопрос не найден");

                if (!QuestionService.BelongsToUser(question, curUser.ID))
                    throw new Exception("Данный вопрос вам не принадлежит");

                if (!QuestionService.SettingAsPaidIsAllowed(question))
                    throw new Exception("Невозможно назначить вознаграждение");

                QuestionService.SetAsPaid(question, setAsPaidFormVM.Price, curUser.ID);

                if (QuestionService.StatusIsApproved(question)
                    && !QuestionService.IsCompleted(question))
                {
                    question.QuestionCompletionJobID = HangfireService.CreateJobForQuestionCompletionDeadline(question);
                    QuestionService.Update(question);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        private void PrepareViewData(List<int> selectedLawAreaIDs)
        {
            ViewData["lawAreas"] = LawAreaService.GetWithLocalizedNameAsIQueryable()
                .Select(m => new SelectListItemWithParent(m))
                .ToList();

            ViewData["languages"] = LanguageService.GetAsSelectList();
        }

        #region Remote validation while creating
        public IActionResult ValidateText(string text, DocumentContentTypesEnum documentContentType)
        {
            return Json(ValidateTextProperty(text, documentContentType));
        }

        private bool ValidateTextProperty(string text, DocumentContentTypesEnum documentContentType)
        {
            bool result = true;
            if (documentContentType == DocumentContentTypesEnum.text && string.IsNullOrWhiteSpace(text))
                result = false;

            return result;
        }

        public IActionResult ValidateAttachment(int? attachmentID, DocumentContentTypesEnum documentContentType)
        {
            return Json(ValidateAttachmentProperty(attachmentID, documentContentType));
        }

        private bool ValidateAttachmentProperty(int? attachmentID, DocumentContentTypesEnum documentContentType)
        {
            bool result = true;
            if (documentContentType == DocumentContentTypesEnum.file && !attachmentID.HasValue)
                result = false;

            return result;
        }
        #endregion
    }
}
