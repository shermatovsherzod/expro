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
    public class QuestionDocumentController : BaseDocumentController
    {
        private readonly IQuestionDocumentAdminActionsService QuestionDocumentAdminActionsService;

        public QuestionDocumentController(
            IQuestionDocumentService questionDocumentService,
            IQuestionDocumentSearchService questionDocumentSearchService,
            IHangfireService hangfireService,
            IDocumentStatusService documentStatusService,
            IQuestionDocumentAdminActionsService questionDocumentAdminActionsService)
            : base(
                  questionDocumentService,
                  documentStatusService,
                  questionDocumentSearchService,
                  questionDocumentAdminActionsService,
                  hangfireService)
        {
            ErrorDocumentNotFound = "Вопрос не найден";

            QuestionDocumentAdminActionsService = questionDocumentAdminActionsService;
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

        public override IActionResult Details(int id)
        {
            return base.Details(id);
        }

        [HttpPost]
        public override IActionResult Approve(int id)
        {
            //return base.Approve(id);
            try
            {
                var document = DocumentService.GetByID(id);
                if (document == null)
                    throw new Exception(ErrorDocumentNotFound);

                var curUser = accountUtil.GetCurrentUser(User);

                if (!DocumentAdminActionsService.ApprovingIsAllowed(document))
                    throw new Exception(ErrorUnableToConfirmDocument);

                //cancel request/video
                QuestionDocumentAdminActionsService.Approve(document, curUser.ID);
                document.QuestionCompletionJobID = HangfireService.CreateJobForQuestionDocumentCompletionDeadline(document);
                DocumentService.Update(document);

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
        public override IActionResult Reject(int id)
        {
            return base.Reject(id);
        }
    }
}
