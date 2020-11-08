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
    [Area("Expert")]
    public class SampleDocumentController : BaseController
    {
        private readonly ISampleDocumentSearchService SampleDocumentSearchService;
        private readonly ILawAreaService LawAreaService;
        private readonly ILanguageService LanguageService;
        private readonly IAttachmentService AttachmentService;
        private readonly ISampleDocumentService SampleDocumentService;
        private readonly IHangfireService HangfireService;
        private readonly IDocumentStatusService DocumentStatusService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SampleDocumentController(
            ISampleDocumentSearchService sampleDocumentSearchService,
            ILawAreaService lawAreaService,
            ILanguageService languageService,
            IAttachmentService attachmentService,
            ISampleDocumentService sampleDocumentService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService,
            UserManager<ApplicationUser> userManager)
        {
            SampleDocumentSearchService = sampleDocumentSearchService;
            LawAreaService = lawAreaService;
            LanguageService = languageService;
            AttachmentService = attachmentService;
            SampleDocumentService = sampleDocumentService;
            HangfireService = hangfireService;
            DocumentStatusService = documentStatusService;
            _userManager = userManager;
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
            //ApplicationUser user = await _userManager.GetUserAsync(User);

            IQueryable<Document> dataIQueryable = SampleDocumentSearchService.Search(
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
                .ToList()
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
        public IActionResult CreateFree(DocumentFreeCreateVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);

                    var model = modelVM.ToModel();
                    SampleDocumentService.Add(model, curUser.ID);

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
        public IActionResult CreatePaid(DocumentPaidCreateVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);

                    var model = modelVM.ToModel();
                    SampleDocumentService.Add(model, curUser.ID);

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
            var model = SampleDocumentService.GetByID(id);
            if (model == null)
                throw new Exception("Документ не найден");

            var modelVM = new DocumentFreeEditVM(model);

            var selectedLawAreaIDs = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();

            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public IActionResult EditFree(DocumentFreeEditVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var modelFromDB = SampleDocumentService.GetByID(modelVM.ID);
                    if (modelFromDB == null)
                        throw new Exception("Документ не найден");

                    var curUser = accountUtil.GetCurrentUser(User);

                    if (!SampleDocumentService.BelongsToUser(modelFromDB, curUser.ID))
                        throw new Exception("Данный документ вам не принадлежит");

                    if (!SampleDocumentService.EditingIsAllowed(modelFromDB))
                        throw new Exception("Статус документа не позволяет отредактировать его");


                    var model = modelVM.ToModel(modelFromDB);
                    LawAreaService.UpdateDocumentLawAreas(model, modelVM.LawAreas);
                    modelVM.LawAreas = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();

                    if (modelVM.ActionType == DocumentActionTypesEnum.submitForApproval)
                    {
                        SampleDocumentService.SubmitForApproval(model, curUser.ID);
                        model.RejectionJobID = HangfireService.CreateJobForDocumentRejectionDeadline(model);
                        SampleDocumentService.Update(model);

                        modelVM.StatusID = (int)DocumentStatusesEnum.WaitingForApproval;
                    }
                    else
                        SampleDocumentService.Update(model, curUser.ID);

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
            var model = SampleDocumentService.GetByID(id);
            if (model == null)
                throw new Exception("Документ не найден");

            var modelVM = new DocumentPaidEditVM(model);

            var selectedLawAreaIDs = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();
            PrepareViewData(selectedLawAreaIDs);

            return View(modelVM);
        }

        [HttpPost]
        public IActionResult EditPaid(DocumentPaidEditVM modelVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var modelFromDB = SampleDocumentService.GetByID(modelVM.ID);
                    if (modelFromDB == null)
                        throw new Exception("Документ не найден");

                    var curUser = accountUtil.GetCurrentUser(User);

                    if (!SampleDocumentService.BelongsToUser(modelFromDB, curUser.ID))
                        throw new Exception("Данный документ вам не принадлежит");

                    if (!SampleDocumentService.EditingIsAllowed(modelFromDB))
                        throw new Exception("Статус документа не позволяет отредактировать его");

                    var model = modelVM.ToModel(modelFromDB);
                    LawAreaService.UpdateDocumentLawAreas(model, modelVM.LawAreas);
                    modelVM.LawAreas = model.DocumentLawAreas.Select(m => m.LawAreaID).ToList();

                    if (modelVM.ActionType == DocumentActionTypesEnum.submitForApproval)
                    {
                        SampleDocumentService.SubmitForApproval(model, curUser.ID);
                        model.RejectionJobID = HangfireService.CreateJobForDocumentRejectionDeadline(model);
                        SampleDocumentService.Update(model);

                        modelVM.StatusID = (int)DocumentStatusesEnum.WaitingForApproval;
                    }
                    else
                        SampleDocumentService.Update(model, curUser.ID);

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
