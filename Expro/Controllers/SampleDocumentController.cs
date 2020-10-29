using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Controllers;
using Expro.Models;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public SampleDocumentController(
            IDocumentService documentService,
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager)
        {
            DocumentService = documentService;
            UserBalanceService = userBalanceService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var documents = DocumentService.GetSampleDocumentsApproved().ToList();

            var documentVMs = new List<SampleDocumentListItemForSiteVM>();
            foreach (var item in documents)
            {
                documentVMs.Add(new SampleDocumentListItemForSiteVM(item));
            }

            return View(documentVMs);
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

            return View(documentVM);
        }
    }
}
