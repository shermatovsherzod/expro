using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Controllers
{
    public class QuestionDocumentController : BaseDocumentController
    {
        private readonly IDocumentAnswerService DocumentAnswerService;

        public QuestionDocumentController(
            IQuestionDocumentService questionDocumentService,
            IQuestionDocumentSearchService questionDocumentSearchService,
            IUserBalanceService userBalanceService,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService,
            IDocumentAnswerService documentAnswerService)
            : base(
                  questionDocumentService,
                  questionDocumentSearchService,
                  userBalanceService,
                  userPurchasedDocumentService,
                  userManager,
                  lawAreaService,
                  documentCounterService)
        {
            DocumentType = DocumentTypesEnum.SampleDocument.ToString();
            ErrorDocumentNotFound = "Образцовый документ не найден";

            DocumentAnswerService = documentAnswerService;
        }

        public override IActionResult Index()
        {
            return base.Index();
        }

        [HttpPost]
        public override IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, 
            DocumentPriceTypesEnum? priceType = null,
            int[] lawAreas = null)
        {
            return base.Search(draw, start, length, statusID, priceType, lawAreas);
        }

        public async override Task<IActionResult> Details(int id)
        {
            var document = DocumentService.GetApprovedWithAnswersAndCommentsByID(id);
            if (document == null)
                throw new Exception(ErrorDocumentNotFound);

            DocumentCounterService.IncrementNumberOfViews(document);

            QuestionDetailsForSiteVM documentVM = new QuestionDetailsForSiteVM(document);

            return View(documentVM);
        }

        //ajax
        [HttpPost]
        public IActionResult AddAnswer(/*[FromBody]*/ QuestionAnswerCreateVM answerCreateVM)
        {
            try
            {
                var curUser = accountUtil.GetCurrentUser(User);
                var document = DocumentService.GetApprovedByID(answerCreateVM.DocumentID);
                if (document == null)
                    throw new Exception("Вопрос не найден");

                DocumentAnswer answer = answerCreateVM.ToModel();
                DocumentAnswerService.Add(answer, curUser.ID);

                return Ok(new { id = answer.ID });
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        //[HttpPost]
        //public override async Task<IActionResult> Purchase(DocumentPurchaseFormVM purchaseFormVM)
        //{
        //    return await base.Purchase(purchaseFormVM);
        //}
    }
}
