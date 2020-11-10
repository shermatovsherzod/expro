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
    public class PracticeDocumentController : BaseDocumentController
    {
        public PracticeDocumentController(
            IPracticeDocumentSearchService practiceDocumentSearchService,
            ILawAreaService lawAreaService,
            ILanguageService languageService,
            IAttachmentService attachmentService,
            IPracticeDocumentService practiceDocumentService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService)
            : base(
                  practiceDocumentSearchService,
                  lawAreaService,
                  languageService,
                  attachmentService,
                  practiceDocumentService,
                  hangfireService,
                  documentStatusService)
        {
        }

        public override IActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public override IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, DocumentPriceTypesEnum? priceType = null)
        {
            return base.Search(draw, start, length, statusID, priceType);
        }

        public override IActionResult CreateFree()
        {
            return base.CreateFree();
        }

        [HttpPost]
        public override IActionResult CreateFree(DocumentFreeCreateVM modelVM)
        {
            return base.CreateFree(modelVM);
        }

        public override IActionResult CreatePaid()
        {
            return base.CreatePaid();
        }

        [HttpPost]
        public override IActionResult CreatePaid(DocumentPaidCreateVM modelVM)
        {
            return base.CreatePaid(modelVM);
        }

        public override IActionResult EditFree(int id)
        {
            return base.EditFree(id);
        }

        [HttpPost]
        public override IActionResult EditFree(DocumentFreeEditVM modelVM)
        {
            return base.EditFree(modelVM);
        }

        public override IActionResult EditPaid(int id)
        {
            return base.EditPaid(id);
        }

        [HttpPost]
        public override IActionResult EditPaid(DocumentPaidEditVM modelVM)
        {
            return base.EditPaid(modelVM);
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
