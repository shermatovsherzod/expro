using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Controllers
{
    public class SampleDocumentController : BaseController
    {
        private readonly IDocumentService DocumentService;

        public SampleDocumentController(IDocumentService documentService)
        {
            DocumentService = documentService;
        }

        public IActionResult Index()
        {
            var documents = DocumentService.GetSampleDocumentsApproved().ToList();

            var documentVMs = new List<SampleDocumentListItemForSiteVM>();
            foreach (var item in documents)
            {
                documentVMs.Add(new SampleDocumentListItemForSiteVM(item));
            }

            return View(documentVMs);
        }

        public IActionResult Details(int id)
        {
            var document = DocumentService.GetSampleDocumentApprovedByID(id);
            if (document == null)
                throw new Exception("Намунавий хужжат не найден");

            SampleDocumentDetailsForSiteVM documentVM = new SampleDocumentDetailsForSiteVM(document);

            return View(documentVM);
        }
    }
}
