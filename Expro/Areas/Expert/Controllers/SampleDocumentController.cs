using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class SampleDocumentController : BaseController
    {
        private readonly ILawAreaService LawAreaService;
        private readonly ILanguageService LanguageService;
        private readonly IAttachmentService AttachmentService;
        private readonly IDocumentService DocumentService;
        private readonly IHangfireService HangfireService;

        public SampleDocumentController(
            ILawAreaService lawAreaService,
            ILanguageService languageService,
            IAttachmentService attachmentService,
            IDocumentService documentService,
            IHangfireService hangfireService)
        {
            LawAreaService = lawAreaService;
            LanguageService = languageService;
            AttachmentService = attachmentService;
            DocumentService = documentService;
            HangfireService = hangfireService;
        }

        public IActionResult Index()
        {
            var curUser = accountUtil.GetCurrentUser(User);
            var documents = DocumentService.GetSampleDocumentsForExpert(curUser.ID).ToList();

            var documentVMs = new List<SampleDocumentListItemForExpertVM>();
            foreach (var item in documents)
            {
                documentVMs.Add(new SampleDocumentListItemForExpertVM(item));
            }

            return View(documentVMs);
        }

        public IActionResult CreateFree()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFree(SampleDocumentFreeCreateVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);

                    var model = modelVM.ToModel();
                    DocumentService.AddSampleDocument(model, curUser.ID);

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
        public IActionResult CreatePaid(SampleDocumentPaidCreateVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);

                    var model = modelVM.ToModel();
                    DocumentService.AddSampleDocument(model, curUser.ID);

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
            var model = DocumentService.GetByID(id);
            if (model == null)
                throw new Exception("Документ не найден");

            var modelVM = new SampleDocumentFreeEditVM(model);

            var selectedLawAreaIDs = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();

            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public IActionResult EditFree(SampleDocumentFreeEditVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var modelFromDB = DocumentService.GetByID(modelVM.ID);
                    if (modelFromDB == null)
                        throw new Exception("Документ не найден");

                    var curUser = accountUtil.GetCurrentUser(User);

                    if (!DocumentService.BelongsToUser(modelFromDB, curUser.ID))
                        throw new Exception("Данный документ вам не принадлежит");

                    if (!DocumentService.EditingIsAllowed(modelFromDB))
                        throw new Exception("Статус документа не позволяет отредактировать его");


                    var model = modelVM.ToModel(modelFromDB);
                    LawAreaService.UpdateDocumentLawAreas(model, modelVM.LawAreas);

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
                //else
                //{
                //    ModelState.AddModelError("Text", "text must be provided");
                //    ModelState.AddModelError("", "please, provide text");
                //}
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

        public IActionResult EditPaid(int id)
        {
            var model = DocumentService.GetByID(id);
            if (model == null)
                throw new Exception("Документ не найден");

            var modelVM = new SampleDocumentPaidEditVM(model);

            var selectedLawAreaIDs = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();
            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public IActionResult EditPaid(SampleDocumentPaidEditVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var modelFromDB = DocumentService.GetByID(modelVM.ID);
                    if (modelFromDB == null)
                        throw new Exception("Документ не найден");

                    var curUser = accountUtil.GetCurrentUser(User);

                    if (!DocumentService.BelongsToUser(modelFromDB, curUser.ID))
                        throw new Exception("Данный документ вам не принадлежит");

                    if (!DocumentService.EditingIsAllowed(modelFromDB))
                        throw new Exception("Статус документа не позволяет отредактировать его");

                    var model = modelVM.ToModel(modelFromDB);
                    LawAreaService.UpdateDocumentLawAreas(model, modelVM.LawAreas);

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
                //else
                //{
                //    ModelState.AddModelError("Text", "text must be provided");
                //    ModelState.AddModelError("", "please, provide text");
                //}
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
