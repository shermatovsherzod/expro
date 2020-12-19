﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class FinancePanelController : BaseController
    {
        private readonly IUserBalanceService UserBalanceService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserPurchasedDocumentService UserPurchasedDocumentService;
        private readonly IQuestionService QuestionService;
        private readonly IWithdrawRequestService WithdrawRequestService;
        private readonly IClickTransactionService ClickTransactionService;
        private readonly IQuestionAnswerService QuestionAnswerService;

        public FinancePanelController(
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            IQuestionService questionService,
            IWithdrawRequestService withdrawRequestService,
            IClickTransactionService clickTransactionService,
            IQuestionAnswerService questionAnswerService)
        {
            UserBalanceService = userBalanceService;
            _userManager = userManager;
            UserPurchasedDocumentService = userPurchasedDocumentService;
            QuestionService = questionService;
            WithdrawRequestService = withdrawRequestService;
            ClickTransactionService = clickTransactionService;
            QuestionAnswerService = questionAnswerService;
        }

        public async Task<IActionResult> Index()
        {
            var curUser = accountUtil.GetCurrentUser(User);
            ApplicationUser user = await _userManager.GetUserAsync(User);
            int userBalance = UserBalanceService.GetBalance(user);

            var userPurchases = UserPurchasedDocumentService.GetPurchasesByUser(curUser.ID);
            var userWithdrawRequests = WithdrawRequestService.GetAllByCreator(curUser.ID);
            //var userQuestionsWithFeeDistributed = QuestionService.GetAllWhereFeeIsDistributedByCreator(curUser.ID);
            var userDocumentsSold = UserPurchasedDocumentService.GetSalesByUser(curUser.ID);
            var userPaidAnswers = QuestionAnswerService.GetManyPaidByAnswerer(curUser.ID);

            ExpertFinancePanelVM financePanelVM = new ExpertFinancePanelVM(
                user,
                userBalance,
                userPurchases.ToList(),
                null,
                userWithdrawRequests.ToList(),
                userDocumentsSold.ToList(),
                userPaidAnswers.ToList());

            return View(financePanelVM);
        }
    }
}