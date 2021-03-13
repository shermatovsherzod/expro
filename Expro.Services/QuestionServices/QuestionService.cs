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
    public class QuestionService : BaseAuthorableService<Question>, IQuestionService
    {
        private IQuestionRepository _questionRepository;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        protected AppConfiguration AppConfiguration { get; set; }

        public QuestionService(IQuestionRepository repository,
                           IUnitOfWork unitOfWork,
                           IEmailService emailService,
                           IUserService userService,
                           IOptionsSnapshot<AppConfiguration> settings = null)
            : base(repository, unitOfWork)
        {
            _questionRepository = repository;
            _emailService = emailService;
            _userService = userService;

            if (settings != null)
                AppConfiguration = settings.Value;
        }

        public override void Add(Question entity, string creatorID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Pending;
            base.Add(entity, creatorID);
        }

        public IQueryable<Question> GetManyWithRelatedDataAsIQueryable()
        {            
            return _questionRepository.GetManyWithRelatedDataAsIQueryable();
        }

        public Question GetFreeByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (!IsFree(model))
                    model = null;
            }

            return model;
        }

        public Question GetPaidByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (model.PriceType != DocumentPriceTypesEnum.Paid)
                    model = null;
            }

            return model;
        }

        bool StatusIsPending(Question entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.Pending;
        }

        public bool StatusIsApproved(Question entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.Approved;
        }

        public bool EditingIsAllowed(Question entity)
        {
            return StatusIsPending(entity);
        }

        public bool BelongsToUser(Question entity, string userID)
        {
            if (entity == null)
                return false;

            return entity.CreatedBy == userID;
        }

        public bool AttachedFileIsAllowedToBeDeleted(Question entity)
        {
            return EditingIsAllowed(entity);
        }

        public void Publish(Question entity, string userID)
        {
            var now = DateTime.Now;
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Approved;
            entity.DateApproved = now;
            entity.DatePublished = now;

            if (!IsFree(entity))
            {
                entity.QuestionCompletionDeadline = DateTimeUtils.RoundToUp(entity.DatePublished.Value
                    .AddMinutes(AppConfiguration.QuestionCompletionDeadlinePeriodInMinutes));
            }

            Update(entity, userID);
        }

        public async void SubmitForApproval(Question entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.WaitingForApproval;
            entity.DateSubmittedForApproval = DateTime.Now;

            entity.RejectionDeadline = DateTimeUtils.RoundToUp(entity.DateSubmittedForApproval.Value
                .AddMinutes(AppConfiguration.QuestionRejectionDeadlinePeriodInMinutes));

            Update(entity, userID);

            try
            {
                List<string> adminEmails = _userService.GetAdminEmails();
                List<Tuple<string, string>> adminEmailsWithNames = new List<Tuple<string, string>>();
                foreach (var item in adminEmails)
                {
                    adminEmailsWithNames.Add(new Tuple<string, string>(item, "Админ"));
                }

                string subjectUz = "Янги савол";
                string subjectRu = "Новый вопрос";
                if (IsFree(entity))
                {
                    subjectUz = "Янги пуллик савол";
                    subjectRu = "Новый вопрос с вознаграждением";
                }

                string questionUrl = "/Admin/Question/Details/" + entity.ID;
                string messageUz = "Янги савол тасдиқлашга жўнатилинди. <a href='" + questionUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Поступил новый вопрос на подтверждение. <a href='" + questionUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    adminEmailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }

        public IQueryable<Question> GetAllForAdmin()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.DocumentStatusID != (int)DocumentStatusesEnum.Pending);
        }

        public IQueryable<Question> GetAllApproved()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.DocumentStatusID == (int)DocumentStatusesEnum.Approved);
        }

        public Question GetApprovedByID(int id)
        {
            var model = GetByID(id);
            if (model != null)
            {
                if (model.DocumentStatusID != (int)DocumentStatusesEnum.Approved)
                    model = null;
            }

            return model;
        }

        private Question GeWithAnswersAndCommentsByID(int id)
        {
            var model = _questionRepository.GeWithAnswersAndCommentsByID(id);
            return model;
        }

        public Question GetApprovedWithAnswersAndCommentsByID(int id)
        {
            var model = GeWithAnswersAndCommentsByID(id);

            if (model != null)
            {
                if (model.DocumentStatusID != (int)DocumentStatusesEnum.Approved)
                    model = null;
            }

            return model;
        }

        public IQueryable<Question> GetAllByCreator(string userID)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.CreatedBy == userID);
        }

        public bool IsFree(Question model)
        {
            if (model == null)
                throw new Exception("Question is null");

            return model.PriceType == DocumentPriceTypesEnum.Free;
        }

        public IQueryable<Question> GetAllAnsweredByUser(string answeredUserID)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.Answers
                    .Select(n => n.CreatedBy)
                    .Contains(answeredUserID));
        }

        public async void CompleteWithDistribution(Question question, string userID)
        {
            question.QuestionFeeIsDistributed = true;
            Complete(question, userID);

            var questionAnswers = question.Answers.ToList();
            foreach (var answer in questionAnswers)
            {
                if (answer.PaidFee.HasValue && answer.PaidFee > 0)
                {
                    try
                    {
                        string answerGiverFullName = answer.Creator.FirstName + " " + answer.Creator.LastName;
                        List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                        emailsWithNames.Add(new Tuple<string, string>(answer.Creator.Email, answerGiverFullName));

                        string subjectUz = "Саволга жавоб учун мукофот олинди";
                        string subjectRu = "Получено вознаграждение за ответ на вопрос";

                        string questionUrl = "/Question/Details/" + question.ID;
                        string messageUz = "Саволга (<a href='" + questionUrl + "'>\"" + question.Title + "\"</a>) жавоб учун " + answer.PaidFee.Value + " сум мукофот олинди.";
                        string messageRu = "Получено вознаграждение в размере " + answer.PaidFee.Value + " сум за ответил на вопрос (<a href='" + questionUrl + "'>\"" + question.Title + "\"</a>)";

                        await _emailService.SendAutomaticallyGeneratedEmailAsync(
                            emailsWithNames,
                            subjectUz, subjectRu,
                            messageUz, messageRu);
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public void Complete(Question question, string userID)
        {
            question.QuestionIsCompleted = true;
            question.DateQuestionCompleted = DateTime.Now;

            if (string.IsNullOrWhiteSpace(userID))
                Update(question);
            else
                Update(question, userID);
        }

        public bool AdminIsAllowedToComplete(Question question)
        {
            var now = DateTime.Now;

            if (!question.DateApproved.HasValue)
                return false;

            if (question.DateApproved.Value.AddDays(1) > now)
                //if (question.DateApproved.Value.AddMinutes(10) < now)
                return true;

            return false;
        }

        public bool IsCompleted(Question question)
        {
            return question.QuestionIsCompleted.HasValue ?
                question.QuestionIsCompleted.Value : false;
        }

        public IQueryable<Question> GetAllWhereFeeIsDistributedByCreator(string creatorID)
        {
            return GetAsIQueryable()
                .Where(m => m.CreatedBy == creatorID 
                    && m.QuestionFeeIsDistributed == true);
        }

        public IQueryable<Question> GetRandomQuestions(int count)
        {
            return GetAllApproved().OrderByDescending(m => m.ID).Take(count);
            //List<int> allApprovedIDs = GetAllApproved().Select(m => m.ID).ToList();

            //List<int> randomlySelectedIDs = RandomUtils.ExtractRandomInts(allApprovedIDs, count);

            //return GetAllApproved().Where(m => randomlySelectedIDs.Contains(m.ID));
        }

        public bool SettingAsPaidIsAllowed(Question question)
        {
            return StatusIsPending(question) || StatusIsApproved(question) && !IsCompleted(question);
        }

        public void SetAsPaid(Question question, int fee, string userID)
        {
            if (StatusIsPending(question))
            {
                question.PriceType = DocumentPriceTypesEnum.Paid;
                question.Price = fee;
                Update(question, userID);
            }
            else if (StatusIsApproved(question))
            {
                if (!IsCompleted(question))
                {
                    question.PriceType = DocumentPriceTypesEnum.Paid;
                    question.Price = fee;
                    question.DatePublished = DateTime.Now;

                    question.QuestionCompletionDeadline = DateTimeUtils.RoundToUp(question.DatePublished.Value
                        .AddMinutes(AppConfiguration.QuestionCompletionDeadlinePeriodInMinutes));

                    Update(question, userID);
                }
            }
        }
    }
}