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
    public class ArticleDocumentController : BaseController
    {
        private readonly IArticleDocumentService ArticleDocumentService;
        private readonly IArticleDocumentSearchService ArticleDocumentSearchService;
        private readonly IHangfireService HangfireService;
        private readonly IDocumentStatusService DocumentStatusService;
        private readonly IDocumentAdminActionsService DocumentAdminActionsService;

        public ArticleDocumentController(
            IArticleDocumentService sampleDocumentService,
            IArticleDocumentSearchService articleDocumentSearchService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService,
            IDocumentAdminActionsService documentAdminActionsService)
        {
            ArticleDocumentService = sampleDocumentService;
            ArticleDocumentSearchService = articleDocumentSearchService;
            HangfireService = hangfireService;
            DocumentStatusService = documentStatusService;
            DocumentAdminActionsService = documentAdminActionsService;
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

            IQueryable<Document> dataIQueryable = ArticleDocumentSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType.Value,
                statusID,
                priceType,
                null,
                null,
                null
            );

            dynamic data = dataIQueryable
                .ToList()
                .Select(m => new DocumentListItemForAdminVM(m))
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
            var document = ArticleDocumentService.GetByID(id);
            if (document == null)
                throw new Exception("Намунавий хужжат не найден");

            DocumentDetailsForAdminVM documentVM = new DocumentDetailsForAdminVM(document);

            return View(documentVM);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            try
            {
                var document = ArticleDocumentService.GetByID(id);
                if (document == null)
                    throw new Exception("Намунавий хужжат не найден");

                var curUser = accountUtil.GetCurrentUser(User);

                if (!DocumentAdminActionsService.ApprovingIsAllowed(document))
                    throw new Exception("Статус хужжата не позволяет подтвердить его");

                //cancel request/video
                DocumentAdminActionsService.Approve(document, curUser.ID);

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
                var document = ArticleDocumentService.GetByID(id);
                if (document == null)
                    throw new Exception("Намунавий хужжат не найден");

                var curUser = accountUtil.GetCurrentUser(User);

                if (!DocumentAdminActionsService.RejectingIsAllowed(document))
                    throw new Exception("Статус хужжата не позволяет отменить его");

                //cancel request/video
                DocumentAdminActionsService.Reject(document, curUser.ID);

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
