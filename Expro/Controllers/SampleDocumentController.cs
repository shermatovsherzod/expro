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
    public class SampleDocumentController : BaseController
    {
        private readonly IDocumentService DocumentService;
        private readonly IUserBalanceService UserBalanceService;
        private readonly IUserPurchasedDocumentService UserPurchasedDocumentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILawAreaService LawAreaService;

        public SampleDocumentController(
            IDocumentService documentService,
            IUserBalanceService userBalanceService,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService)
        {
            DocumentService = documentService;
            UserBalanceService = userBalanceService;
            UserPurchasedDocumentService = userPurchasedDocumentService;
            _userManager = userManager;
            LawAreaService = lawAreaService;
        }

        public IActionResult Index()
        {
            var documents = DocumentService.GetSampleDocumentsApproved().ToList();

            var documentVMs = new List<SampleDocumentListItemForSiteVM>();
            foreach (var item in documents)
            {
                documentVMs.Add(new SampleDocumentListItemForSiteVM(item));
            }

            ViewData["lawAreas"] = LawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = false,
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            return View(documentVMs);
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

                null,
                statusID,
                priceType,
                curUser.ID,
                null
            );

            dynamic data = dataIQueryable
                .ToList()
                .Select(m => new SampleDocumentListItemForSiteVM(m))
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

        public async Task<IActionResult> Details(int id)
        {
            var document = DocumentService.GetSampleDocumentApprovedByID(id);
            if (document == null)
                throw new Exception("Намунавий хужжат не найден");

            DocumentService.IncrementNumberOfViews(document);

            SampleDocumentDetailsForSiteVM documentVM = new SampleDocumentDetailsForSiteVM(document);

            if (!DocumentService.IsFree(document))
            {
                var curUser = accountUtil.GetCurrentUser(User);
                if (curUser != null)
                {
                    ApplicationUser user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        if (UserPurchasedDocumentService.UserPurchasedDocumentBefore(user, document))
                            ViewData["purchasedBefore"] = true;
                        else
                        {
                            int curUserBalance = UserBalanceService.GetBalance(user);
                            ViewData["curUserBalance"] = curUserBalance;
                            ViewData["curUserBalanceStr"] = curUserBalance
                                .ToString(AppData.Configuration.NumberViewStringFormat)
                                .Trim();

                            if (curUserBalance < documentVM.Price)
                            {
                                int paymentAmount = documentVM.Price - curUserBalance;
                                string returnUrl = "https://expro.uz/SampleDocument/Details/" + id;
                                ViewData["returnUrl"] = returnUrl;
                                ViewData["paymentAmount"] = paymentAmount;
                                ViewData["paymentAmountStr"] = paymentAmount
                                    .ToString(AppData.Configuration.NumberViewStringFormat)
                                    .Trim();
                                ViewData["clickPaymentButtonUrl"] = UserBalanceService
                                    .GenerateClickPaymentButtonUrl(user.AccountNumber, paymentAmount, returnUrl);
                            }
                        }
                    }
                }
            }

            return View(documentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(SampleDocumentPurchaseFormVM purchaseFormVM)
        {
            //once purchased, redirect to /User/SampleDocument/Details/id
            var document = DocumentService.GetSampleDocumentApprovedByID(purchaseFormVM.DocumentID);
            if (document == null)
                throw new Exception("Намунавий хужжат не найден");

            if (DocumentService.IsFree(document))
                throw new Exception("Намунавий хужжат бесплатный!");

            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
                throw new Exception("Вы не авторизованы");

            int curUserBalance = UserBalanceService.GetBalance(user);
            if (curUserBalance < document.Price)
                throw new Exception("Недостаточно средств на балансе");

            UserPurchasedDocumentService.Purchase(user, document);
            DocumentService.IncrementNumberOfPurchases(document);

            return Redirect("/User/SampleDocument/Details/" + document.ID);
        }
    }
}
