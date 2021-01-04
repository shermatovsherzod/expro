using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
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

            return View();
        }

        [HttpPost]
        public virtual IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null, UserTypesEnum? userType = null)
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
                userType,
                statusID,
                curUser.ID
            );

            dynamic data = dataIQueryable
                .Select(m => new WithdrawRequestListItemForAdminVM(m))
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

        [HttpPost]
        public virtual IActionResult MarkAsCompleted(int id)
        {
            try
            {
                var request = WithdrawRequestService.GetByID(id);
                if (request == null)
                    throw new Exception("Запрос не найден");

                var curUser = accountUtil.GetCurrentUser(User);

                if (WithdrawRequestService.IsCompleted(request))
                    throw new Exception("Запрос ранее уже был выполнен");

                WithdrawRequestService.MarkAsCompleted(request, curUser.ID);

                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}
