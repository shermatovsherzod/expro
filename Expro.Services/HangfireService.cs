using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Services
{
    public class HangfireService : IHangfireService
    {
        private readonly IDocumentService DocumentService;
        private readonly IQuestionService QuestionService;
        private readonly IDocumentAdminActionsService DocumentAdminActionsService;
        private readonly IQuestionAdminActionsService QuestionAdminActionsService;
        private readonly IUserBalanceService UserBalanceService;
        private readonly IRatingUpdateService _ratingUpdateService;

        public HangfireService(
            IDocumentService documentService,
            IQuestionService questionService,
            IDocumentAdminActionsService documentAdminActionsService,
            IQuestionAdminActionsService questionAdminActionsService,
            IUserBalanceService userBalanceService,
            IRatingUpdateService ratingUpdateService)
        {
            DocumentService = documentService;
            QuestionService = questionService;
            DocumentAdminActionsService = documentAdminActionsService;
            QuestionAdminActionsService = questionAdminActionsService;
            UserBalanceService = userBalanceService;
            _ratingUpdateService = ratingUpdateService;
        }

        public string CreateJobForDocumentRejectionDeadline(Document document)
        {
            string jobID = BackgroundJob.Schedule(() =>
                DocumentRejectionDeadlineReaches(document.ID),
                new DateTimeOffset(document.RejectionDeadline.Value));

            return jobID;
        }

        public void DocumentRejectionDeadlineReaches(int documentID)
        {
            //try
            //{
            Document document = DocumentService.GetByID(documentID);

            //if (document.PriceType == DocumentPriceTypesEnum.Paid)
            //    UserBalanceService.ReplenishBalance(document.Creator, document.Price.Value);

            DocumentAdminActionsService.RejectionDeadlineReaches(document);
            //}
            //catch (Exception ex)
            //{ }
        }

        public string CreateJobForQuestionRejectionDeadline(Question question)
        {
            string jobID = BackgroundJob.Schedule(() =>
                QuestionRejectionDeadlineReaches(question.ID),
                new DateTimeOffset(question.RejectionDeadline.Value));

            return jobID;
        }

        public void QuestionRejectionDeadlineReaches(int questionID)
        {
            //try
            //{
            Question question = QuestionService.GetByID(questionID);

            if (question.PriceType == DocumentPriceTypesEnum.Paid)
                UserBalanceService.ReplenishBalance(question.Creator, question.Price.Value);

            QuestionAdminActionsService.RejectionDeadlineReaches(question);
            //}
            //catch (Exception ex)
            //{ }
        }

        public string CreateJobForQuestionCompletionDeadline(Question question)
        {
            string jobID = BackgroundJob.Schedule(() =>
                QuestionDocumentCompletionDeadlineReaches(question.ID),
                new DateTimeOffset(question.QuestionCompletionDeadline.Value));

            return jobID;
        }

        public void QuestionDocumentCompletionDeadlineReaches(int documentID)
        {
            //try
            //{
            Question question = QuestionService.GetByID(documentID);

            if (question.PriceType == DocumentPriceTypesEnum.Paid)
            {
                UserBalanceService.ReplenishBalance(question.Creator, question.Price.Value);
                QuestionAdminActionsService.CompletionDeadlineReaches(question);
            }
                
            
            //}
            //catch (Exception ex)
            //{ }
        }

        public void CreateRecurringJobForUpdatingRatingsForAllExperts()
        {
            RecurringJob.AddOrUpdate("99999", () => CallRatingUpdateMethod(), Cron.Daily, TimeZoneInfo.Local);
        }

        public void CallRatingUpdateMethod()
        {
            try
            {
                _ratingUpdateService.UpdateRatingForAllExperts();
            }
            catch (Exception ex)
            {
                // email about error can be sent
            }
        }

        public void CancelJob(string jobID)
        {
            if (!string.IsNullOrWhiteSpace(jobID))
            {
                //try
                //{
                BackgroundJob.Delete(jobID);
                //}
                //catch
                //{ }
            }
        }
    }
}
