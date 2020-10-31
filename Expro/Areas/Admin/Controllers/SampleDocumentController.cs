using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SampleDocumentController : BaseController
    {
        private readonly IDocumentService DocumentService;
        private readonly IHangfireService HangfireService;

        public SampleDocumentController(
            IDocumentService documentService,
            IHangfireService hangfireService)
        {
            DocumentService = documentService;
            HangfireService = hangfireService;
        }

        public IActionResult Index()
        {
            var documents = DocumentService.GetSampleDocumentsForAdmin().ToList();

            var documentVMs = new List<SampleDocumentListItemForAdminVM>();
            foreach (var item in documents)
            {
                documentVMs.Add(new SampleDocumentListItemForAdminVM(item));
            }

            return View(documentVMs);
        }

        public IActionResult Details(int id)
        {
            var document = DocumentService.GetSampleDocumentByID(id);
            if (document == null)
                throw new Exception("Намунавий хужжат не найден");

            SampleDocumentDetailsForAdminVM documentVM = new SampleDocumentDetailsForAdminVM(document);

            return View(documentVM);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            try
            {
                var document = DocumentService.GetSampleDocumentByID(id);
                if (document == null)
                    throw new Exception("Намунавий хужжат не найден");

                var curUser = accountUtil.GetCurrentUser(User);

                if (!DocumentService.ApprovingIsAllowed(document))
                    throw new Exception("Статус хужжата не позволяет подтвердить его");

                //cancel request/video
                DocumentService.Approve(document, curUser.ID);

                //cancel hangfire jobs
                HangfireService.CancelJob(document.RejectionJobID);

                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            try
            {
                var document = DocumentService.GetSampleDocumentByID(id);
                if (document == null)
                    throw new Exception("Намунавий хужжат не найден");

                var curUser = accountUtil.GetCurrentUser(User);

                if (!DocumentService.RejectingIsAllowed(document))
                    throw new Exception("Статус хужжата не позволяет отменить его");

                //cancel request/video
                DocumentService.Reject(document, curUser.ID);

                //cancel hangfire jobs
                HangfireService.CancelJob(document.RejectionJobID);

                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}
