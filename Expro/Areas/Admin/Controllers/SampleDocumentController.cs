using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
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
        private readonly IDocumentStatusService DocumentStatusService;

        public SampleDocumentController(
            IDocumentService documentService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService)
        {
            DocumentService = documentService;
            HangfireService = hangfireService;
            DocumentStatusService = documentStatusService;
        }

        public IActionResult Index()
        {
            //var documents = DocumentService.GetSampleDocumentsForAdmin().ToList();

            //var documentVMs = new List<SampleDocumentListItemForAdminVM>();
            //foreach (var item in documents)
            //{
            //    documentVMs.Add(new SampleDocumentListItemForAdminVM(item));
            //}

            //return View(documentVMs);

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

            IQueryable<Document> dataIQueryable = DocumentService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType.Value,
                statusID,
                priceType,
                null,
                null
            );

            dynamic data = dataIQueryable
                .ToList()
                .Select(m => new SampleDocumentListItemForAdminVM(m))
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
