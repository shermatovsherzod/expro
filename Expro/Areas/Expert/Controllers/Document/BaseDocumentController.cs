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

namespace Expro.Areas.Expert.Controllers
{
    //[Area("Expert")]
    public class BaseDocumentController : BaseController
    {
        private readonly IDocumentSearchService DocumentSearchService;
        private readonly ILawAreaService LawAreaService;
        private readonly ILanguageService LanguageService;
        private readonly IAttachmentService AttachmentService;
        private readonly IDocumentService DocumentService;
        private readonly IHangfireService HangfireService;
        private readonly IDocumentStatusService DocumentStatusService;
        private readonly IUserService _userService;

        public BaseDocumentController(
            IDocumentSearchService documentSearchService,
            ILawAreaService lawAreaService,
            ILanguageService languageService,
            IAttachmentService attachmentService,
            IDocumentService documentService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService,
            IUserService userService)
        {
            DocumentSearchService = documentSearchService;
            LawAreaService = lawAreaService;
            LanguageService = languageService;
            AttachmentService = attachmentService;
            DocumentService = documentService;
            HangfireService = hangfireService;
            DocumentStatusService = documentStatusService;
            _userService = userService;
        }

        public virtual IActionResult Index()
        {
            ViewData["statuses"] = DocumentStatusService.GetAsSelectList();

            return View();
        }

        [HttpPost]
        public virtual IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, DocumentPriceTypesEnum? priceType = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);

            IQueryable<Document> dataIQueryable = DocumentSearchService.Search(
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

        public virtual IActionResult CreateFree()
        {
            return View();
        }

        [HttpPost]
        public virtual IActionResult CreateFree(DocumentFreeCreateVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);

                    var model = modelVM.ToModel();
                    DocumentService.Add(model, curUser.ID);

                    return RedirectToAction("EditFree", new { id = model.ID });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Что-то пошло не так: " + ex.Message);
            }

            return View(modelVM);
        }

        public virtual IActionResult CreatePaid()
        {
            var curUser = accountUtil.GetCurrentUser(User);
            var appUser = _userService.GetByID(curUser.ID);
            if (appUser == null)
                throw new Exception("Пользователь не найден");

            ViewData["userIsAllowedToWorkWithPaidMaterials"] = 
                _userService.UserIsAllowedToWorkWithPaidMaterials(appUser);

            return View();
        }

        [HttpPost]
        public virtual IActionResult CreatePaid(DocumentPaidCreateVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);
                    var appUser = _userService.GetByID(curUser.ID);
                    if (appUser == null)
                        throw new Exception("Пользователь не найден");

                    bool userIsAllowedToWorkWithPaidMaterials =
                        _userService.UserIsAllowedToWorkWithPaidMaterials(appUser);

                    if (!userIsAllowedToWorkWithPaidMaterials)
                    {
                        ViewData["userIsAllowedToWorkWithPaidMaterials"] = false;
                        throw new Exception("user is not allowed to create paid document");
                    }

                    var model = modelVM.ToModel();
                    DocumentService.Add(model, curUser.ID);

                    return RedirectToAction("EditPaid", new { id = model.ID });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Что-то пошло не так: " + ex.Message);
            }

            return View(modelVM);
        }

        public virtual IActionResult EditFree(int id)
        {
            var model = DocumentService.GetFreeByID(id);
            if (model == null)
                throw new Exception("Документ не найден");

            var modelVM = new DocumentFreeEditVM(model);

            var selectedLawAreaIDs = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();

            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public virtual IActionResult EditFree(DocumentFreeEditVM modelVM)
        {
            return EditSharedPost(modelVM, DocumentPriceTypesEnum.Free);
        }

        public virtual IActionResult EditPaid(int id)
        {
            var model = DocumentService.GetPaidByID(id);
            if (model == null)
                throw new Exception("Документ не найден");

            var modelVM = new DocumentPaidEditVM(model);

            var selectedLawAreaIDs = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();
            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public virtual IActionResult EditPaid(DocumentPaidEditVM modelVM)
        {
            return EditSharedPost(modelVM, DocumentPriceTypesEnum.Paid);
        }

        private IActionResult EditSharedPost(DocumentFreeEditVM modelVM, DocumentPriceTypesEnum priceType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Document modelFromDB;
                    if (priceType == DocumentPriceTypesEnum.Free)
                        modelFromDB = DocumentService.GetFreeByID(modelVM.ID);
                    else
                        modelFromDB = DocumentService.GetPaidByID(modelVM.ID);

                    if (modelFromDB == null)
                        throw new Exception("Документ не найден");

                    var curUser = accountUtil.GetCurrentUser(User);

                    if (!DocumentService.BelongsToUser(modelFromDB, curUser.ID))
                        throw new Exception("Данный документ вам не принадлежит");

                    if (!DocumentService.EditingIsAllowed(modelFromDB))
                        throw new Exception("Статус документа не позволяет отредактировать его");

                    if (modelVM.ContentType == DocumentContentTypesEnum.file)
                    {
                        if (!modelVM.AttachmentID.HasValue)
                        {
                            ModelState.AddModelError("AttachmentID", "Загрузите файл");
                            throw new Exception("Загрузите файл");
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(modelVM.Text))
                        {
                            ModelState.AddModelError("Text", "Наберите текст");
                            throw new Exception("Наберите текст");
                        }
                    }

                    var model = modelVM.ToModel(modelFromDB);
                    LawAreaService.UpdateDocumentLawAreas(model, modelVM.LawAreas);
                    modelVM.LawAreas = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();

                    if (modelVM.ActionType == DocumentActionTypesEnum.submitForApproval)
                    {
                        DocumentService.SubmitForApproval(model, curUser.ID);
                        model.RejectionJobID = HangfireService.CreateJobForDocumentRejectionDeadline(model);
                        DocumentService.Update(model);

                        modelVM.StatusID = (int)DocumentStatusesEnum.WaitingForApproval;
                    }
                    else
                        DocumentService.Update(model, curUser.ID);

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
