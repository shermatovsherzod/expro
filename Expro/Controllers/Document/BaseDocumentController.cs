﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Expro.Controllers
{
    public class BaseDocumentController : BaseController
    {
        protected readonly IDocumentService DocumentService;
        private readonly IDocumentSearchService DocumentSearchService;
        protected readonly IUserBalanceService UserBalanceService;
        private readonly IUserPurchasedDocumentService UserPurchasedDocumentService;
        protected readonly IUserService _userService;
        private readonly ILawAreaService LawAreaService;
        protected readonly IDocumentCounterService DocumentCounterService;
        private readonly IUserRatingService _userRatingService;

        protected string DocumentType = "";
        protected string ErrorDocumentNotFound = "";

        public BaseDocumentController(
            IDocumentService documentService,
            IDocumentSearchService documentSearchService,
            IUserBalanceService userBalanceService,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            IUserService userService,
            ILawAreaService lawAreaService,
            IDocumentCounterService documentCounterService,
            IUserRatingService userRatingService,
            IStringLocalizer<Resources.ResourceTexts> localizer)
        {
            DocumentService = documentService;
            DocumentSearchService = documentSearchService;
            UserBalanceService = userBalanceService;
            UserPurchasedDocumentService = userPurchasedDocumentService;
            _userService = userService;
            LawAreaService = lawAreaService;
            DocumentCounterService = documentCounterService;
            _userRatingService = userRatingService;
            _localizer = localizer;

            ErrorDocumentNotFound = _localizer["DocumentNotFound"];
        }

        public virtual IActionResult Index()
        {
            ViewData["lawAreas"] = LawAreaService.GetWithLocalizedNameAsIQueryable()
                .Select(m => new SelectListItemWithParent(m))
                .ToList();

            return View();
        }

        [HttpPost]
        public virtual IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, 
            DocumentPriceTypesEnum? priceType = null,
            int? lawAreaParent = null,
            int[] lawAreas = null)
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

                null,
                statusID,
                priceType,
                null,
                null,
                lawAreaParent,
                lawAreas
            );

            dynamic data = dataIQueryable
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

        public virtual IActionResult Details(int id)
        {
            var document = DocumentService.GetApprovedByID(id);
            if (document == null)
                throw new Exception(ErrorDocumentNotFound);

            if (DocumentService.IsFree(document))
            {
                ApplicationUser expert = document.Creator;
                int points = DocumentService.PointsForDocumentFreeView;
                _userRatingService.AddPointsToUser(points, expert);
            }
            DocumentCounterService.IncrementNumberOfViews(document);

            var curUser = accountUtil.GetCurrentUser(User);
            DocumentDetailsForSiteVM documentVM = new DocumentDetailsForSiteVM(document, curUser.ID);

            if (!DocumentService.IsFree(document))
            {
                if (curUser.ID != null)
                {
                    ApplicationUser user = _userService.GetByID(curUser.ID);
                    if (user != null)
                    {
                        if (UserPurchasedDocumentService.UserPurchasedDocumentBefore(user, document))
                        {
                            ViewData["purchasedBefore"] = true;
                            ViewData["curUserArea"] = curUser.UserArea;
                        }
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
                                string returnUrl = "https://expro.uz/" + DocumentType + "/Details/" + id;
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

            ViewData["curPageUrl"] = Request.Path.Value;

            bool showAdminActions = false;
            if (User.Identity.IsAuthenticated)
            {
                var curUserr = accountUtil.GetCurrentUser(User);
                if (curUserr.IsAdmin)
                    showAdminActions = true;
            }
            ViewData["showAdminActions"] = showAdminActions;

            return View(documentVM);
        }

        [Authorize()]
        [HttpPost]
        public virtual IActionResult Purchase(DocumentPurchaseFormVM purchaseFormVM)
        {
            var document = DocumentService.GetApprovedByID(purchaseFormVM.DocumentID);
            if (document == null)
                throw new Exception(ErrorDocumentNotFound);

            if (DocumentService.IsFree(document))
                throw new Exception(_localizer["DocumentIsFree"] + "!");

            var curUser = accountUtil.GetCurrentUser(User);
            ApplicationUser user = _userService.GetByID(curUser.ID);
            if (user == null)
                throw new Exception(_localizer["YouAreNotAuthorized"]);

            int curUserBalance = UserBalanceService.GetBalance(user);
            if (curUserBalance < document.Price)
                throw new Exception(_localizer["NotEnougthMoneyOnBalance"]);

            UserPurchasedDocumentService.Purchase(user, document);

            ApplicationUser expert = document.Creator;
            int points = DocumentService.PointsForDocumentPurchase;
            _userRatingService.AddPointsToUser(points, expert);
            DocumentCounterService.IncrementNumberOfPurchases(document);

            return Redirect("/" + curUser.UserArea.Value.ToString() + "/" + DocumentType + "Purchased/Details/" + document.ID);
        }
    }
}
