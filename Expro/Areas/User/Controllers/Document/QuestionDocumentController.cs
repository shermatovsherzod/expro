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
    public class QuestionDocumentController : BaseController
    {
        private readonly IQuestionDocumentSearchService QuestionDocumentSearchService;
        private readonly ILawAreaService LawAreaService;
        private readonly ILanguageService LanguageService;
        private readonly IAttachmentService AttachmentService;
        private readonly IQuestionDocumentService QuestionDocumentService;
        private readonly IHangfireService HangfireService;
        private readonly IDocumentStatusService DocumentStatusService;
        private readonly IUserBalanceService UserBalanceService;

        public QuestionDocumentController(
            IQuestionDocumentSearchService questionDocumentSearchService,
            ILawAreaService lawAreaService,
            ILanguageService languageService,
            IAttachmentService attachmentService,
            IQuestionDocumentService questionDocumentService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService,
            IUserBalanceService userBalanceService)
        {
            QuestionDocumentSearchService = questionDocumentSearchService;
            LawAreaService = lawAreaService;
            LanguageService = languageService;
            AttachmentService = attachmentService;
            QuestionDocumentService = questionDocumentService;
            HangfireService = hangfireService;
            DocumentStatusService = documentStatusService;
            UserBalanceService = userBalanceService;
        }

        public IActionResult Index()
        {
            ViewData["statuses"] = DocumentStatusService.GetAsSelectList();

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

            IQueryable<Document> dataIQueryable = QuestionDocumentSearchService.Search(
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
                .Select(m => new DocumentListItemForExpertVM(m))
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
                    QuestionDocumentService.Add(model, curUser.ID);

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
                    QuestionDocumentService.Add(model, curUser.ID);

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
            var model = QuestionDocumentService.GetFreeByID(id);
            if (model == null)
                throw new Exception("Вопрос не найден");

            var modelVM = new QuestionFreeEditVM(model);

            var selectedLawAreaIDs = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();

            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public IActionResult EditFree(QuestionFreeEditVM modelVM)
        {
            //modelVM.ContentType = DocumentContentTypesEnum.text;

            return EditSharedPost(modelVM, DocumentPriceTypesEnum.Free);
        }


        public IActionResult EditPaid(int id)
        {
            var model = QuestionDocumentService.GetPaidByID(id);
            if (model == null)
                throw new Exception("Вопрос не найден");

            var modelVM = new QuestionPaidEditVM(model);

            var selectedLawAreaIDs = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();
            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public IActionResult EditPaid(QuestionPaidEditVM modelVM)
        {
            //modelVM.ContentType = DocumentContentTypesEnum.text;

            return EditSharedPost(modelVM, DocumentPriceTypesEnum.Paid);
        }

        private IActionResult EditSharedPost(QuestionFreeEditVM modelVM, DocumentPriceTypesEnum priceType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Document modelFromDB;
                    if (priceType == DocumentPriceTypesEnum.Free)
                        modelFromDB = QuestionDocumentService.GetFreeByID(modelVM.ID);
                    else
                        modelFromDB = QuestionDocumentService.GetPaidByID(modelVM.ID);

                    if (modelFromDB == null)
                        throw new Exception("Вопрос не найден");

                    var curUser = accountUtil.GetCurrentUser(User);

                    if (!QuestionDocumentService.BelongsToUser(modelFromDB, curUser.ID))
                        throw new Exception("Данный вопрос вам не принадлежит");

                    if (!QuestionDocumentService.EditingIsAllowed(modelFromDB))
                        throw new Exception("Статус вопроса не позволяет отредактировать его");

                    //if (modelVM.ContentType == DocumentContentTypesEnum.file)
                    //{
                    //    if (!modelVM.AttachmentID.HasValue)
                    //    {
                    //        ModelState.AddModelError("AttachmentID", "Загрузите файл");
                    //        throw new Exception("Загрузите файл");
                    //    }
                    //}
                    //else
                    //{
                    //    if (string.IsNullOrWhiteSpace(modelVM.Text))
                    //    {
                    //        ModelState.AddModelError("Text", "Наберите текст");
                    //        throw new Exception("Наберите текст");
                    //    }
                    //}

                    var model = modelVM.ToModel(modelFromDB);
                    LawAreaService.UpdateDocumentLawAreas(model, modelVM.LawAreas);
                    modelVM.LawAreas = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();

                    if (modelVM.ActionType == DocumentActionTypesEnum.submitForApproval)
                    {
                        QuestionDocumentService.SubmitForApproval(model, curUser.ID);
                        //UserBalanceService.TakeOffBalance()
                        model.RejectionJobID = HangfireService.CreateJobForDocumentRejectionDeadline(model);
                        QuestionDocumentService.Update(model);

                        modelVM.StatusID = (int)DocumentStatusesEnum.WaitingForApproval;
                    }
                    else
                        QuestionDocumentService.Update(model, curUser.ID);

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
