using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    public class SampleDocumentController : BaseController
    {
        private readonly IDocumentService DocumentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SampleDocumentController(
            IDocumentService documentService,
            UserManager<ApplicationUser> userManager)
        {
            DocumentService = documentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var documents = DocumentService.GetDocumentsPurchasedByUser(user).ToList();

            var documentVMs = new List<SampleDocumentListItemForUserVM>();
            foreach (var item in documents)
            {
                documentVMs.Add(new SampleDocumentListItemForUserVM(item));
            }

            return View(documentVMs);
        }

        public IActionResult Details(int id)
        {
            var document = DocumentService.GetSampleDocumentByID(id);
            if (document == null)
                throw new Exception("Намунавий хужжат не найден");

            DocumentService.IncrementNumberOfViews(document);

            SampleDocumentDetailsForUserVM documentVM = new SampleDocumentDetailsForUserVM(document);

            return View(documentVM);
        }
    }
}
