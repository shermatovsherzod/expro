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
    public class FinancePanelController : BaseController
    {
        private readonly IUserBalanceService UserBalanceService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserPurchasedDocumentService UserPurchasedDocumentService;
        private readonly IQuestionService QuestionService;
        private readonly IWithdrawRequestService WithdrawRequestService;
        private readonly IClickTransactionService ClickTransactionService;
        private readonly IQuestionAnswerService QuestionAnswerService;
        private readonly IUserService _userService;

        public FinancePanelController(
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager,
            IUserPurchasedDocumentService userPurchasedDocumentService,
            IQuestionService questionService,
            IWithdrawRequestService withdrawRequestService,
            IClickTransactionService clickTransactionService,
            IQuestionAnswerService questionAnswerService,
            IUserService userService)
        {
            UserBalanceService = userBalanceService;
            _userManager = userManager;
            UserPurchasedDocumentService = userPurchasedDocumentService;
            QuestionService = questionService;
            WithdrawRequestService = withdrawRequestService;
            ClickTransactionService = clickTransactionService;
            QuestionAnswerService = questionAnswerService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            List<AppUserVM> usersVM = new List<AppUserVM>();

            var users = _userService.GetAllExpertsAndSimpleUsers();

            foreach (var item in users)
            {
                usersVM.Add(new AppUserVM(item));
            }

            return View(usersVM);
        }

        public async Task<IActionResult> Details(string userID)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userID);
            if (user == null)
                throw new Exception("Пользователь не найден");

            int userBalance = UserBalanceService.GetBalance(user);

            if (user.UserType == UserTypesEnum.Expert)
            {
                var userPurchases = UserPurchasedDocumentService.GetPurchasesByUser(user.Id);
                var userWithdrawRequests = WithdrawRequestService.GetAllByCreator(user.Id);
                var userDocumentsSold = UserPurchasedDocumentService.GetSalesByUser(user.Id);
                var userPaidAnswers = QuestionAnswerService.GetManyPaidByAnswerer(user.Id);

                ExpertFinancePanelVM financePanelVM = new ExpertFinancePanelVM(
                    user,
                    userBalance,
                    userPurchases.ToList(),
                    null,
                    userWithdrawRequests.ToList(),
                    userDocumentsSold.ToList(),
                    userPaidAnswers.ToList());

                return View("DetailsOfExpert", financePanelVM);
            }
            else if (user.UserType == UserTypesEnum.SimpleUser)
            {
                var userPurchases = UserPurchasedDocumentService.GetPurchasesByUser(user.Id);
                var userQuestionsWithFeeDistributed = QuestionService.GetAllWhereFeeIsDistributedByCreator(user.Id);
                var userWithdrawRequests = WithdrawRequestService.GetAllByCreator(user.Id);
                var clickTransactions = ClickTransactionService.GetAllByCreator(user.Id);

                UserFinancePanelVM financePanelVM = new UserFinancePanelVM(
                    user,
                    userBalance,
                    userPurchases.ToList(),
                    userQuestionsWithFeeDistributed.ToList(),
                    userWithdrawRequests.ToList(),
                    clickTransactions.ToList());

                return View("DetailsOfUser", financePanelVM);
            }
            else
                throw new Exception("Пользователь не найден");
        }
    }
}
