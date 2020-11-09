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
    public class SampleDocumentController : BaseDocumentController
    {
        //private readonly ISampleDocumentService SampleDocumentService;
        //private readonly ISampleDocumentSearchService SampleDocumentSearchService;
        //private readonly IHangfireService HangfireService;
        //private readonly IDocumentStatusService DocumentStatusService;
        //private readonly IDocumentAdminActionsService DocumentAdminActionsService;

        public SampleDocumentController(
            ISampleDocumentService sampleDocumentService,
            ISampleDocumentSearchService sampleDocumentSearchService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService,
            IDocumentAdminActionsService documentAdminActionsService)
            : base(
                  sampleDocumentService,
                  documentStatusService,
                  sampleDocumentSearchService,
                  documentAdminActionsService,
                  hangfireService)
        {
            //SampleDocumentService = sampleDocumentService;
            //SampleDocumentSearchService = sampleDocumentSearchService;
            //HangfireService = hangfireService;
            //DocumentStatusService = documentStatusService;
            //DocumentAdminActionsService = documentAdminActionsService;

            ErrorDocumentNotFound = "Образцовый документ не найден";
        }

        public override IActionResult Index()
        {
            return base.Index();
            //ViewData["statuses"] = DocumentStatusService.GetAsSelectList();
            //return View();
        }

        [HttpPost]
        public override IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, DocumentPriceTypesEnum? priceType = null)
        {
            return base.Search(draw, start, length, statusID, priceType);
            //int recordsTotal = 0;
            //int recordsFiltered = 0;
            //string error = "";

            //var curUser = accountUtil.GetCurrentUser(User);
            ////ApplicationUser user = await _userManager.GetUserAsync(User);

            //IQueryable<Document> dataIQueryable = SampleDocumentSearchService.Search(
            //    start,
            //    length,

            //    out recordsTotal,
            //    out recordsFiltered,
            //    out error,

            //    curUser.UserType.Value,
            //    statusID,
            //    priceType,
            //    null,
            //    null,
            //    null
            //);

            //dynamic data = dataIQueryable
            //    .ToList()
            //    .Select(m => new DocumentListItemForAdminVM(m))
            //    .ToList();

            //return Json(new
            //{
            //    draw = draw,
            //    recordsTotal = recordsTotal,
            //    recordsFiltered = recordsFiltered,
            //    data = data,
            //    error = error
            //});
        }

        public override IActionResult Details(int id)
        {
            return base.Details(id);
            //var document = SampleDocumentService.GetByID(id);
            //if (document == null)
            //    throw new Exception("Намунавий хужжат не найден");

            //DocumentDetailsForAdminVM documentVM = new DocumentDetailsForAdminVM(document);

            //return View(documentVM);
        }

        [HttpPost]
        public override IActionResult Approve(int id)
        {
            return base.Approve(id);
            //try
            //{
            //    var document = SampleDocumentService.GetByID(id);
            //    if (document == null)
            //        throw new Exception("Намунавий хужжат не найден");

            //    var curUser = accountUtil.GetCurrentUser(User);

            //    if (!DocumentAdminActionsService.ApprovingIsAllowed(document))
            //        throw new Exception("Статус хужжата не позволяет подтвердить его");

            //    //cancel request/video
            //    DocumentAdminActionsService.Approve(document, curUser.ID);

            //    //cancel hangfire jobs
            //    HangfireService.CancelJob(document.RejectionJobID);

            //    return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return CustomBadRequest(ex);
            //}
        }

        [HttpPost]
        public override IActionResult Reject(int id)
        {
            return base.Reject(id);
            //try
            //{
            //    var document = SampleDocumentService.GetByID(id);
            //    if (document == null)
            //        throw new Exception("Намунавий хужжат не найден");

            //    var curUser = accountUtil.GetCurrentUser(User);

            //    if (!DocumentAdminActionsService.RejectingIsAllowed(document))
            //        throw new Exception("Статус хужжата не позволяет отменить его");

            //    //cancel request/video
            //    DocumentAdminActionsService.Reject(document, curUser.ID);

            //    //cancel hangfire jobs
            //    HangfireService.CancelJob(document.RejectionJobID);

            //    return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return CustomBadRequest(ex);
            //}
        }
    }
}
