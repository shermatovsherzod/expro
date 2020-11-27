﻿using System;
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

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class WithdrawRequestController : BaseController
    {
        private readonly IWithdrawRequestService WithdrawRequestService;
        private readonly IWithdrawRequestStatusService WithdrawRequestStatusService;
        private readonly IUserBalanceService UserBalanceService;
        private readonly UserManager<ApplicationUser> _userManager;

        public WithdrawRequestController(
            IWithdrawRequestService withdrawRequestService,
            IWithdrawRequestStatusService withdrawRequestStatusService,
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager)
        {
            WithdrawRequestService = withdrawRequestService;
            WithdrawRequestStatusService = withdrawRequestStatusService;
            UserBalanceService = userBalanceService;
            _userManager = userManager;
        }

        public IActionResult Index(bool? successfullyCreated)
        {
            ViewData["statuses"] = WithdrawRequestStatusService.GetAsSelectList();
            ViewData["successfullyCreated"] = successfullyCreated;

            return View();
        }

        [HttpPost]
        public virtual IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null)
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);

            IQueryable<WithdrawRequest> dataIQueryable = WithdrawRequestService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType.Value,
                null,
                statusID,
                curUser.ID
            );

            dynamic data = dataIQueryable
                .Select(m => new WithdrawRequestListItemForExpertVM(m))
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

        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            
            int userBalance = UserBalanceService.GetBalance(user);
            if (WithdrawRequestService.UserHasNotEnoughtMoneyForWithdrawal(userBalance))
            {
                ViewData["userHasNotEnoughtMoneyForWithdrawal"] = true;
                ViewData["minimalAmountInBalanceForWithdrawal"] =
                    WithdrawRequestService.GetMinimalAmountInBalanceForWithdrawal();
            }
            ViewData["userBalance"] = userBalance;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WithdrawRequestCreateVM modelVM)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            try
            {
                if (ModelState.IsValid)
                {
                    var curUser = accountUtil.GetCurrentUser(User);
                    int userBalance = UserBalanceService.GetBalance(user);

                    if (WithdrawRequestService.UserHasNotEnoughtMoneyForWithdrawal(userBalance))
                        throw new Exception("Ваш баланс не позволяет подать заявку на вывод средств");

                    if (UserBalanceService.BalanceIsLessThan(user, modelVM.Amount))
                        throw new Exception("На Вашем балансе недостаточно средство для снятия суммы " + modelVM.Amount);

                    var model = modelVM.ToModel();
                    WithdrawRequestService.Add(model, user);

                    return RedirectToAction("Index", new { successfullyCreated = true });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Что-то пошло не так: " + ex.Message);
            }

            ViewData["userBalance"] = UserBalanceService.GetBalance(user);

            return View(modelVM);
        }
    }
}
