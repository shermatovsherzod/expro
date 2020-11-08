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
    public class ArticleDocumentController : BaseController
    {
        private readonly IArticleDocumentService ArticleDocumentService;
        private readonly IArticleDocumentSearchService ArticleDocumentSearchService;
        private readonly IUserBalanceService UserBalanceService;
        private readonly IUserPurchasedDocumentService UserPurchasedDocumentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILawAreaService LawAreaService;
        private readonly IDocumentCounterService DocumentCounterService;

        public ArticleDocumentController(
            IArticleDocumentService articleDocumentService,
            IArticleDocumentSearchService articleDocumentSearchService,
            IUserBalanceService userBalanceService,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            UserManager<ApplicationUser> userManager,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService)
        {
            ArticleDocumentService = articleDocumentService;
            ArticleDocumentSearchService = articleDocumentSearchService;
            UserBalanceService = userBalanceService;
            UserPurchasedDocumentService = userPurchasedDocumentService;
            _userManager = userManager;
            LawAreaService = lawAreaService;
            DocumentCounterService = documentCounterService;
        }

        public IActionResult Index()
        {
            ViewData["lawAreas"] = LawAreaService.GetAsIQueryable()
                .Select(m => new SelectListItemWithParent()
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                    Selected = false,
                    ParentValue = m.ParentID.HasValue ? m.ParentID.Value.ToString() : ""
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, 
            DocumentPriceTypesEnum? priceType = null,
            int[] lawAreas = null)
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

                null,
                statusID,
                priceType,
                curUser.ID,
                null,
                lawAreas
            );

            dynamic data = dataIQueryable
                .ToList()
                .Select(m => new DocumentListItemForSiteVM(m))
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
            var document = ArticleDocumentService.GetApprovedByID(id);
            if (document == null)
                throw new Exception("Намунавий хужжат не найден");

            DocumentCounterService.IncrementNumberOfViews(document);

            DocumentDetailsForSiteVM documentVM = new DocumentDetailsForSiteVM(document);

            if (!ArticleDocumentService.IsFree(document))
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
                                string returnUrl = "https://expro.uz/ArticleDocument/Details/" + id;
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
        public async Task<IActionResult> Purchase(DocumentPurchaseFormVM purchaseFormVM)
        {
            //once purchased, redirect to /User/ArticleDocument/Details/id
            var document = ArticleDocumentService.GetApprovedByID(purchaseFormVM.DocumentID);
            if (document == null)
                throw new Exception("Намунавий хужжат не найден");

            if (ArticleDocumentService.IsFree(document))
                throw new Exception("Намунавий хужжат бесплатный!");

            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
                throw new Exception("Вы не авторизованы");

            int curUserBalance = UserBalanceService.GetBalance(user);
            if (curUserBalance < document.Price)
                throw new Exception("Недостаточно средств на балансе");

            UserPurchasedDocumentService.Purchase(user, document);
            DocumentCounterService.IncrementNumberOfPurchases(document);

            return Redirect("/User/ArticleDocumentPurchased/Details/" + document.ID);
        }
    }
}
