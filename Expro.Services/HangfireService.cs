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
        private readonly IEmailService _emailService;

        public HangfireService(
            IDocumentService documentService,
            IQuestionService questionService,
            IDocumentAdminActionsService documentAdminActionsService,
            IQuestionAdminActionsService questionAdminActionsService,
            IUserBalanceService userBalanceService,
            IRatingUpdateService ratingUpdateService,
            IEmailService emailService)
        {
            DocumentService = documentService;
            QuestionService = questionService;
            DocumentAdminActionsService = documentAdminActionsService;
            QuestionAdminActionsService = questionAdminActionsService;
            UserBalanceService = userBalanceService;
            _ratingUpdateService = ratingUpdateService;
            _emailService = emailService;
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

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = document.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "";
                string subjectRu = "";
                string documentUrl = "";
                string messageUz = "";
                string messageRu = "";

                if (document.DocumentTypeID == (int)DocumentTypesEnum.ArticleDocument)
                {
                    subjectUz = "Мақола администратор томонидан рад этилди";
                    subjectRu = "Статья отменена администратором";

                    documentUrl = "/Expert/ArticleDocument/Details/" + document.ID;
                    messageUz = "Мақола администратор томонидан рад этилди. <a href='" + documentUrl + "'>" + document.Title + "</a>";
                    messageRu = "Статья отменена администратором. <a href='" + documentUrl + "'>" + document.Title + "</a>";
                }
                else if (document.DocumentTypeID == (int)DocumentTypesEnum.SampleDocument)
                {
                    subjectUz = "Намунавий хужжат администратор томонидан рад этилди";
                    subjectRu = "Образцовый документ отменен администратором";

                    documentUrl = "/Expert/SampleDocument/Details/" + document.ID;
                    messageUz = "Намунавий хужжат администратор томонидан рад этилди. <a href='" + documentUrl + "'>" + document.Title + "</a>";
                    messageRu = "Образцовый документ отменен администратором. <a href='" + documentUrl + "'>" + document.Title + "</a>";
                }
                else if (document.DocumentTypeID == (int)DocumentTypesEnum.PracticeDocument)
                {
                    subjectUz = "Амалиёт администратор томонидан рад этилди";
                    subjectRu = "Практический документ отменен администратором";

                    documentUrl = "/Expert/PracticeDocument/Details/" + document.ID;
                    messageUz = "Амалиёт администратор томонидан рад этилди. <a href='" + documentUrl + "'>" + document.Title + "</a>";
                    messageRu = "Практический документ отменен администратором. <a href='" + documentUrl + "'>" + document.Title + "</a>";
                }

                if (!string.IsNullOrWhiteSpace(subjectUz))
                {
                    _emailService.SendEmailAsync(
                        emailsWithNames,
                        subjectUz, subjectRu,
                        messageUz, messageRu);
                }
            }
            catch (Exception ex) { }
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

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = question.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Савол администратор томонидан рад этилди";
                string subjectRu = "Вопрос отменен администратором";

                string questionUrl = "/User/Question/Details/" + question.ID;
                string messageUz = "Савол администратор томонидан рад этилди. <a href='" + questionUrl + "'>" + question.Title + "</a>";
                string messageRu = "Вопрос отменен администратором. <a href='" + questionUrl + "'>" + question.Title + "</a>";

                _emailService.SendEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
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
