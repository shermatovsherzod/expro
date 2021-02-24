using Expro.Common;
using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public UserStatusService(IUserStatusRepository repository,
                          IUnitOfWork unitOfWork,
                          UserManager<ApplicationUser> userManager,
                          IEmailService emailService,
                           IOptionsSnapshot<AppConfiguration> settings = null)
           : base(repository, unitOfWork)
        {
            _userManager = userManager;
            _emailService = emailService;
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
                List<Tuple<string, string>> adminEmailsWithNames = new List<Tuple<string, string>>();
                foreach (var item in adminEmails)
                {
                    adminEmailsWithNames.Add(new Tuple<string, string>(item, "Админ"));
                }

                string subjectUz = "Эксперт тасдиқлашга жўнатилди";
                string subjectRu = "Запрос на подтверждение эксперта";

                string messageUz = "Эксперт тасдиқлашга жўнатилди. <a href='/Admin/Experts/Details/" + user.Id + "'>" + user.FirstName + " " + user.LastName + "</a>";
                string messageRu = "Запрос на подтверждение эксперта.  <a href='/Admin/Experts/Details/" + user.Id + "'>" + user.FirstName + " " + user.LastName + "</a>";

                await _emailService.SendEmailAsync(
                    adminEmailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);

                return true;
            }
            return false;
        }
    }
}