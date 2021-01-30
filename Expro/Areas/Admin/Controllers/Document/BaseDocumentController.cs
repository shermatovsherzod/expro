using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers
{
    //[Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class BaseDocumentController : BaseController
    {
        protected readonly IDocumentService DocumentService;
        private readonly IDocumentStatusService DocumentStatusService;
        private readonly IDocumentSearchService DocumentSearchService;
        protected readonly IDocumentAdminActionsService DocumentAdminActionsService;
        protected readonly IHangfireService HangfireService;
        private readonly IUserService _userService;
        private readonly IUserRatingService _userRatingService;

        protected string ErrorDocumentNotFound = "Документ не найден";
        protected string ErrorUnableToConfirmDocument = "Статус документа не позволяет подтвердить его";

        public BaseDocumentController(
            IDocumentService documentService,
            IDocumentStatusService documentStatusService,
            IDocumentSearchService documentSearchService,
            IDocumentAdminActionsService documentAdminActionsService,
            IHangfireService hangfireService,
            IUserService userService,
            IUserRatingService userRatingService)
        {
            DocumentService = documentService;
            DocumentStatusService = documentStatusService;
            DocumentSearchService = documentSearchService;
            DocumentAdminActionsService = documentAdminActionsService;
            HangfireService = hangfireService;
            _userService = userService;
            _userRatingService = userRatingService;
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

            IQueryable<Document> dataIQueryable = DocumentSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                UserTypesEnum.Admin,
                statusID,
                priceType,
                null,
                null,
                null
            );

            dynamic data = dataIQueryable
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

        public virtual IActionResult Details(int id)
        {
            var document = DocumentService.GetByID(id);
            if (document == null)
                throw new Exception(ErrorDocumentNotFound);

            DocumentDetailsForAdminVM documentVM = new DocumentDetailsForAdminVM(document);

            return View(documentVM);
        }

        [HttpPost]
        public virtual IActionResult Approve(int id)
        {
            try
            {
                var document = DocumentService.GetByID(id);
                if (document == null)
                    throw new Exception(ErrorDocumentNotFound);

                var curUser = accountUtil.GetCurrentUser(User);

                if (!DocumentAdminActionsService.ApprovingIsAllowed(document))
                    throw new Exception(ErrorUnableToConfirmDocument);

                var expert = _userService.GetByID(document.CreatedBy);
                if (expert == null)
                    throw new Exception("Эксперт не найден");

                int points = 0;
                if (DocumentService.IsFree(document))
                    points = DocumentService.PointsForDocumentFree;
                else
                    points = DocumentService.PointsForDocumentPaid;
                _userRatingService.AddPointsToUser(points, expert);

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
        public virtual IActionResult Reject(int id)
        {
            try
            {
                var document = DocumentService.GetByID(id);
                if (document == null)
                    throw new Exception(ErrorDocumentNotFound);

                var curUser = accountUtil.GetCurrentUser(User);

                if (!DocumentAdminActionsService.RejectingIsAllowed(document))
                    throw new Exception(ErrorUnableToConfirmDocument);

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
