using Expro.Models;
using Expro.Models.Enums;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class ExpertStatusViewComponent : ViewComponent
    {
        UserManager<ApplicationUser> _userManager;

        public ExpertStatusViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            AccountUtil accountUtil = new AccountUtil();
            var currentUserAccount = accountUtil.GetCurrentUser(this.HttpContext.User);
            var currentUser = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);

            string result = "";

            switch (currentUser.ApproveStatus)
            {
                //не запрашивал подтверждение аккаунта
                case (int)ExpertApproveStatusEnum.NotApproved:
                    result = "<a onclick='profileConfirmationRequestByExpert()' href='#' class='btn btn-success'>Не подтвержден. Отправить заявку на подтверждение профайла</a>";
                    break;

                //запросил подтверждение но не подтверждено
                case (int)ExpertApproveStatusEnum.WaitingForApproval:
                    result = "<div class='alert alert-info' role='alert'>Ожидание подтверждения аккаунта</div>";
                    break;

                //аккаунт подтвержден
                case (int)ExpertApproveStatusEnum.Approved:
                    result = "<div class='alert alert-success' role='alert'>Аккаунт подтвержден</div>";
                    break;

                //аккаунту отказано
                case (int)ExpertApproveStatusEnum.Rejected:
                    result = "<a onclick='profileConfirmationRequestByExpert()' href='#' class='btn btn-danger'>Отказано. Проверьте свои данные и заново отправьте заявку на подтверждение профайла</a>";
                    break;
                default:
                    break;
            }

            return View("ExpertStatus", result);
            
        }

    }
}
