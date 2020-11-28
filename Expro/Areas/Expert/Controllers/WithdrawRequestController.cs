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

namespace Expro.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class WithdrawRequestController : BaseWithdrawRequestController
    {
        public WithdrawRequestController(
            IWithdrawRequestService withdrawRequestService,
            IWithdrawRequestStatusService withdrawRequestStatusService,
            IUserBalanceService userBalanceService,
            UserManager<ApplicationUser> userManager)
            : base(
                  withdrawRequestService,
                  withdrawRequestStatusService,
                  userBalanceService,
                  userManager)
        {
        }

        public override IActionResult Index(bool? successfullyCreated)
        {
            return base.Index(successfullyCreated);
        }

        [HttpPost]
        public override IActionResult Search(
            int draw, int? start = null, int? length = null,
            int? statusID = null)
        {
            return base.Search(draw, start, length, statusID);
        }

        public override async Task<IActionResult> Create()
        {
            return await base.Create();
        }

        [HttpPost]
        public override async Task<IActionResult> Create(WithdrawRequestCreateVM modelVM)
        {
            return await base.Create(modelVM);
        }
    }
}
