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

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestionDocumentController : BaseDocumentController
    {
        private readonly IQuestionDocumentSearchService QuestionDocumentSearchService;
        private readonly IQuestionDocumentAdminActionsService QuestionDocumentAdminActionsService;
        private readonly IUserBalanceService UserBalanceService;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionDocumentController(
            IQuestionDocumentService questionDocumentService,
            IQuestionDocumentSearchService questionDocumentSearchService,
            IHangfireService hangfireService,
            IQuestionStatusService documentStatusService,
            IQuestionDocumentAdminActionsService questionDocumentAdminActionsService,
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager)
            : base(
                  questionDocumentService,
                  documentStatusService,
                  questionDocumentSearchService,
                  questionDocumentAdminActionsService,
                  hangfireService)
        {
            ErrorDocumentNotFound = "Вопрос не найден";

            QuestionDocumentAdminActionsService = questionDocumentAdminActionsService;
            UserBalanceService = userBalanceService;
            _userManager = userManager;
            QuestionDocumentSearchService = questionDocumentSearchService;
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
            //return base.Search(draw, start, length, statusID, priceType);
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);

            IQueryable<Document> dataIQueryable = QuestionDocumentSearchService.Search(
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
                .Select(m => new QuestionListItemForAdminVM(m))
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
            //return base.Reject(id);
            try
            {
                var document = DocumentService.GetByID(id);
                if (document == null)
                    throw new Exception(ErrorDocumentNotFound);

                var curUser = accountUtil.GetCurrentUser(User);

                if (!DocumentAdminActionsService.RejectingIsAllowed(document))
                    throw new Exception(ErrorUnableToConfirmDocument);

                if (document.PriceType == DocumentPriceTypesEnum.Paid)
                {
                    UserBalanceService.ReplenishBalance(document.Creator, document.Price.Value);
                }
                
                DocumentAdminActionsService.Reject(document, curUser.ID);
                
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
