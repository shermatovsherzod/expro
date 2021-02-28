using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Expro.Services
{
    public class ExpertsListAdminActionsService : IExpertsListAdminActionsService
    {
        UserManager<ApplicationUser> _userManager;
        IUserService _userService;
        private readonly IEmailService _emailService;
        public ExpertsListAdminActionsService()
        {

        }

        public ExpertsListAdminActionsService(UserManager<ApplicationUser> userManager, IUserService userService, IEmailService emailService)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailService;
        }

        public bool Approve(ApplicationUser entity)
        {
            entity.UserStatusID = (int)ExpertApproveStatusEnum.Approved;
            entity.DateApproved = DateTime.Now;

            try
            {
                _userService.Update(entity);              

                string subjectUz = "Эксперт тасдиқланди";
                string subjectRu = "Эксперт подтвержден";

                string messageUz = "Эксперт тасдиқланди.";
                string messageRu = "Эксперт подтвержден.";

                List<Tuple<string, string>> emails = new List<Tuple<string, string>>();

                emails.Add(new Tuple<string, string>(entity.Email, "Пользователь"));

                _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emails,
                    subjectUz, subjectRu,
                    messageUz, messageRu);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Reject(ApplicationUser entity)
        {
            entity.UserStatusID = (int)ExpertApproveStatusEnum.Rejected;
            entity.DateRejected = DateTime.Now;
            try
            {
                _userService.Update(entity);              

                string subjectUz = "Эксперт тасдиқланмади";
                string subjectRu = "Эксперту отказано.";

                string messageUz = "Эксперт тасдиқланмади. Илтимос, маълумотларингизни қайта текширинг.";
                string messageRu = "Эксперту отказано. Пожалуйста, проверьте введенные данные.";
               
                List<Tuple<string, string>> emails = new List<Tuple<string, string>>();

                emails.Add(new Tuple<string, string>(entity.Email, "Пользователь"));               

                _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emails,
                    subjectUz, subjectRu,
                    messageUz, messageRu);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}