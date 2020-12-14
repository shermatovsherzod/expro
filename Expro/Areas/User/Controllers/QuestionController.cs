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

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
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

        public QuestionController(
            IQuestionSearchService questionSearchService,
            ILawAreaService lawAreaService,
            ILanguageService languageService,
            IAttachmentService attachmentService,
            IQuestionService questionService,
            IHangfireService hangfireService,
            IQuestionStatusService questionStatusService,
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager)
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
            return View();
        }

        [HttpPost]
        public IActionResult CreateFree(QuestionFreeCreateVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);

                    var model = modelVM.ToModel();
                    QuestionService.Add(model, curUser.ID);

                    return RedirectToAction("EditFree", new { id = model.ID });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Что-то пошло не так: " + ex.Message);
            }

            return View(modelVM);
        }

        public IActionResult CreatePaid()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePaid(QuestionPaidCreateVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);

                    var model = modelVM.ToModel();
                    QuestionService.Add(model, curUser.ID);

                    return RedirectToAction("EditPaid", new { id = model.ID });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Что-то пошло не так: " + ex.Message);
            }

            return View(modelVM);
        }

        public IActionResult EditFree(int id)
        {
            var model = QuestionService.GetFreeByID(id);
            if (model == null)
                throw new Exception("Вопрос не найден");

            var modelVM = new QuestionFreeEditVM(model);

            var selectedLawAreaIDs = model.QuestionLawAreas.Select(m => m.LawAreaID).ToList();

            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditFree(QuestionFreeEditVM modelVM)
        {
            return await EditSharedPost(modelVM, DocumentPriceTypesEnum.Free);
        }


        public IActionResult EditPaid(int id)
        {
            var model = QuestionService.GetPaidByID(id);
            if (model == null)
                throw new Exception("Вопрос не найден");

            var modelVM = new QuestionPaidEditVM(model);

            var selectedLawAreaIDs = model.QuestionLawAreas.Select(m => m.LawAreaID).ToList();
            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditPaid(QuestionPaidEditVM modelVM)
        {
            return await EditSharedPost(modelVM, DocumentPriceTypesEnum.Paid);
        }

        private async Task<IActionResult> EditSharedPost(QuestionFreeEditVM modelVM, DocumentPriceTypesEnum priceType)
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

                    if (modelVM.ActionType == DocumentActionTypesEnum.submitForApproval)
                    {
                        ApplicationUser curAppUser = await _userManager.GetUserAsync(User);
                        int curUserBalance = UserBalanceService.GetBalance(curAppUser);
                        if (curUserBalance < model.Price)
                            throw new Exception("На Вашем балансе недостаточно средств");

                        QuestionService.SubmitForApproval(model, curUser.ID);

                        if (priceType == DocumentPriceTypesEnum.Paid)
                            UserBalanceService.TakeOffBalance(curAppUser, model.Price.Value);

                        model.RejectionJobID = HangfireService.CreateJobForQuestionRejectionDeadline(model);
                        QuestionService.Update(model);

                        modelVM.StatusID = (int)DocumentStatusesEnum.WaitingForApproval;
                    }
                    else
                        QuestionService.Update(model, curUser.ID);

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

        private void PrepareViewData(List<int> selectedLawAreaIDs)
        {
            ViewData["lawAreas"] = LawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = selectedLawAreaIDs.Contains(m.ID),
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

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
