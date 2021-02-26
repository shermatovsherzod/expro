using Expro.Common;
using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;




using System.Threading.Tasks;

namespace Expro.Services
{
    public class UserStatusService : BaseCRUDService<UserStatus>, IUserStatusService
    {
        UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        protected AppConfiguration AppConfiguration { get; set; }

        private readonly LinkGenerator _linkGenerator;
        IHttpContextAccessor _httpContextAccessor;

        public UserStatusService(IUserStatusRepository repository,
                          IUnitOfWork unitOfWork,
                          UserManager<ApplicationUser> userManager,
                          IEmailService emailService,
                          LinkGenerator linkGenerator,
                          IHttpContextAccessor httpContextAccessor,
                          IOptionsSnapshot<AppConfiguration> settings = null
                         )
           : base(repository, unitOfWork)
        {
            _userManager = userManager;
            _emailService = emailService;
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;


            if (settings != null)
                AppConfiguration = settings.Value;
        }

        public string GetUserStatusText(string userID)
        {
            ApplicationUser user = _userManager.Users.FirstOrDefault(c => c.Id == userID);

            if (user == null)
                throw new Exception("Пользователь не найден");

            return user.UserStatus.Name;
        }

        public async Task<bool> Approve(string userID)
        {
            ApplicationUser user = _userManager.Users.FirstOrDefault(c => c.Id == userID);

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.UserStatusID = (int)ExpertApproveStatusEnum.Approved;
            user.DateApproved = DateTime.Now;
            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Reject(string userID)
        {
            ApplicationUser user = _userManager.Users.FirstOrDefault(c => c.Id == userID);

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.UserStatusID = (int)ExpertApproveStatusEnum.Rejected;
            user.DateRejected = DateTime.Now;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ProfileConfirmationRequestByExpert(string userID)
        {
            ApplicationUser user = _userManager.Users.FirstOrDefault(c => c.Id == userID);

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.UserStatusID = (int)ExpertApproveStatusEnum.WaitingForApproval;
            user.DateSubmittedForApproval = DateTime.Now;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                List<string> adminEmails = AppConfiguration.AdminEmails.Split(';').ToList();

                string _link = _linkGenerator.GetUriByAction(
                    _httpContextAccessor.HttpContext,
                    values: new { area = "Admin", controller = "Experts", action = "Details", id = user.Id }
                    );

                string message = "Запрос на подтверждение эксперта <a href='" + _link + "'>" + user.FirstName + " " + user.LastName + "</a>"; ;

                foreach (var item in adminEmails)
                {
                    await _emailService.SendEmailAsync(item, "Запрос на подтверждение эксперта", message);
                }

                return true;
            }
            return false;
        }


        //public async Task<bool> ProfileConfirmationRequestByExpert(string userID)
        //{
        //    ApplicationUser user = _userManager.Users.FirstOrDefault(c => c.Id == userID);

        //    if (user == null)
        //        throw new Exception("Пользователь не найден");

        //    user.UserStatusID = (int)ExpertApproveStatusEnum.WaitingForApproval;
        //    user.DateSubmittedForApproval = DateTime.Now;

        //    IdentityResult result = await _userManager.UpdateAsync(user);
        //    if (result.Succeeded)
        //    {

        //        List<string> adminEmails = AppConfiguration.AdminEmails.Split(';').ToList();
        //        List<Tuple<string, string>> adminEmailsWithNames = new List<Tuple<string, string>>();
        //        foreach (var item in adminEmails)
        //        {
        //            adminEmailsWithNames.Add(new Tuple<string, string>(item, "Админ"));
        //        }

        //        string subjectUz = "Эксперт тасдиқлашга жўнатилди";
        //        string subjectRu = "Запрос на подтверждение эксперта";

        //        string messageUz = "Эксперт тасдиқлашга жўнатилди. <a href='/Admin/Experts/Details/" + user.Id + "'>" + user.FirstName + " " + user.LastName + "</a>";
        //        string messageRu = "Запрос на подтверждение эксперта.  <a href='/Admin/Experts/Details/" + user.Id + "'>" + user.FirstName + " " + user.LastName + "</a>";



        //        await _emailService.SendEmailAsync(
        //            adminEmailsWithNames,
        //            subjectUz, subjectRu,
        //            messageUz, messageRu);

        //        return true;
        //    }
        //    return false;
        //}
    }
}