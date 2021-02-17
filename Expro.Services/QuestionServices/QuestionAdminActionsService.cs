using Expro.Common;
using Expro.Common.Utilities;
using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Expro.Services
{
    public class QuestionAdminActionsService : BaseApprovableByAdminService<Question>, IQuestionAdminActionsService
    {
        protected IQuestionService _questionService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        protected AppConfiguration AppConfiguration { get; set; }

        public QuestionAdminActionsService(IQuestionRepository repository,
                           IUnitOfWork unitOfWork,
                           IQuestionService questionService,
                           IEmailService emailService,
                           IUserService userService,
                           IOptionsSnapshot<AppConfiguration> settings = null)
            : base(repository, unitOfWork)
        {
            _questionService = questionService;
            _emailService = emailService;
            _userService = userService;

            if (settings != null)
                AppConfiguration = settings.Value;
        }

        public async override void Approve(Question entity, string userID)
        {
            base.Approve(entity, userID);

            entity.QuestionCompletionDeadline = DateTimeUtils.RoundToUp(entity.DateApproved.Value
                .AddMinutes(AppConfiguration.QuestionCompletionDeadlinePeriodInMinutes));

            Update(entity, userID);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = _userService.GetByID(entity.CreatedBy);
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Савол администратор томонидан тасдиқланди";
                string subjectRu = "Вопрос подтвержден администратором";

                string questionUrl = "/User/Question/Details/" + entity.ID;
                string messageUz = "Савол администратор томонидан тасдиқланди. <a href='" + questionUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Вопрос подтвержден администратором. <a href='" + questionUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }

        public async override void Reject(Question entity, string userID)
        {
            base.Reject(entity, userID);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = _userService.GetByID(entity.CreatedBy);
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Савол администратор томонидан рад этилди";
                string subjectRu = "Вопрос отменен администратором";

                string questionUrl = "/User/Question/Details/" + entity.ID;
                string messageUz = "Савол администратор томонидан рад этилди. <a href='" + questionUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Вопрос отменен администратором. <a href='" + questionUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }

        public void CompletionDeadlineReaches(Question Question)
        {
            if (_questionService.IsCompleted(Question))
                return;

            _questionService.Complete(Question, null);
        }
    }
}