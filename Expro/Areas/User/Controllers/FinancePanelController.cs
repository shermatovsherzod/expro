using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Policy = "SimpleUserOnly")]
    public class FinancePanelController : BaseController
    {
        private readonly IUserBalanceService UserBalanceService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserPurchasedDocumentService UserPurchasedDocumentService;
        private readonly IQuestionService QuestionService;
        private readonly IWithdrawRequestService WithdrawRequestService;
        private readonly IClickTransactionService ClickTransactionService;

        public FinancePanelController(
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            IQuestionService questionService,
            IWithdrawRequestService withdrawRequestService,
            IClickTransactionService clickTransactionService)
        {
            UserBalanceService = userBalanceService;
            _userManager = userManager;
            UserPurchasedDocumentService = userPurchasedDocumentService;
            QuestionService = questionService;
            WithdrawRequestService = withdrawRequestService;
            ClickTransactionService = clickTransactionService;
        }

        public async Task<IActionResult> Index()
        {
            var curUser = accountUtil.GetCurrentUser(User);
            ApplicationUser user = await _userManager.GetUserAsync(User);
            int userBalance = UserBalanceService.GetBalance(user);

            var userPurchases = UserPurchasedDocumentService.GetPurchasesByUser(curUser.ID);
            var userQuestionsWithFeeDistributed = QuestionService.GetAllWhereFeeIsDistributedByCreator(curUser.ID);
            var userWithdrawRequests = WithdrawRequestService.GetAllByCreator(curUser.ID);
            var clickTransactions = ClickTransactionService.GetAllByCreator(curUser.ID);

            UserFinancePanelVM financePanelVM = new UserFinancePanelVM(
                user,
                userBalance,
                userPurchases.ToList(),
                userQuestionsWithFeeDistributed.ToList(),
                userWithdrawRequests.ToList(),
                clickTransactions.ToList());

            return View(financePanelVM);
        }
    }
}
