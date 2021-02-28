using Expro.Common;
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
    public class SampleDocumentService : DocumentService, ISampleDocumentService
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public SampleDocumentService(
            IDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IUserService userService,
            IOptionsSnapshot<AppConfiguration> settings = null)
            : base(repository, unitOfWork, settings)
        {
            _documentType = DocumentTypesEnum.SampleDocument;
            PointsForDocumentFree = Constants.PointsFor.DOCUMENT.SAMPLE.FREE;
            PointsForDocumentPaid = Constants.PointsFor.DOCUMENT.SAMPLE.PAID;
            PointsForDocumentFreeView = Constants.PointsFor.DOCUMENT.SAMPLE.FREE_VIEW;
            PointsForDocumentPurchase = Constants.PointsFor.DOCUMENT.SAMPLE.PURCHASE;
            PointsForDocumentFreeLike = Constants.PointsFor.DOCUMENT.SAMPLE.FREE_LIKE;
            PointsForDocumentPaidLike = Constants.PointsFor.DOCUMENT.SAMPLE.PAID_LIKE;

            _emailService = emailService;
            _userService = userService;
        }

        public async override void SubmitForApproval(Document entity, string userID)
        {
            base.SubmitForApproval(entity, userID);

            try
            {
                List<string> adminEmails = _userService.GetAdminEmails();
                List<Tuple<string, string>> adminEmailsWithNames = new List<Tuple<string, string>>();
                foreach (var item in adminEmails)
                {
                    adminEmailsWithNames.Add(new Tuple<string, string>(item, "Админ"));
                }

                string subjectUz = "Янги намунавий хужжат";
                string subjectRu = "Новый образцовый документ";
                if (IsFree(entity))
                {
                    subjectUz = "Янги пуллик намунавий хужжат";
                    subjectRu = "Новый платный образцовый документ";
                }

                string documentUrl = "/Admin/SampleDocument/Details/" + entity.ID;
                string messageUz = "Янги намунавий хужжат тасдиқлашга жўнатилинди. <a href='" + documentUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Поступил новый образцовый документ на подтверждение. <a href='" + documentUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    adminEmailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }
    }

    public class ArticleDocumentService : DocumentService, IArticleDocumentService
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public ArticleDocumentService(
            IDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IUserService userService,
            IOptionsSnapshot<AppConfiguration> settings = null)
            : base(repository, unitOfWork, settings)
        {
            _documentType = DocumentTypesEnum.ArticleDocument;
            PointsForDocumentFree = Constants.PointsFor.DOCUMENT.ARTICLE.FREE;
            PointsForDocumentPaid = Constants.PointsFor.DOCUMENT.ARTICLE.PAID;
            PointsForDocumentFreeView = Constants.PointsFor.DOCUMENT.ARTICLE.FREE_VIEW;
            PointsForDocumentPurchase = Constants.PointsFor.DOCUMENT.ARTICLE.PURCHASE;
            PointsForDocumentFreeLike = Constants.PointsFor.DOCUMENT.ARTICLE.FREE_LIKE;
            PointsForDocumentPaidLike = Constants.PointsFor.DOCUMENT.ARTICLE.PAID_LIKE;

            _emailService = emailService;
            _userService = userService;
        }

        public async override void SubmitForApproval(Document entity, string userID)
        {
            base.SubmitForApproval(entity, userID);

            try
            {
                List<string> adminEmails = _userService.GetAdminEmails();
                List<Tuple<string, string>> adminEmailsWithNames = new List<Tuple<string, string>>();
                foreach (var item in adminEmails)
                {
                    adminEmailsWithNames.Add(new Tuple<string, string>(item, "Админ"));
                }

                string subjectUz = "Янги мақола";
                string subjectRu = "Новая статья";
                if (IsFree(entity))
                {
                    subjectUz = "Янги пуллик мақола";
                    subjectRu = "Новая платная статья";
                }

                string documentUrl = "/Admin/ArticleDocument/Details/" + entity.ID;
                string messageUz = "Янги мақола тасдиқлашга жўнатилинди. <a href='" + documentUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Поступила новая статья на подтверждение. <a href='" + documentUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    adminEmailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }
    }

    public class PracticeDocumentService : DocumentService, IPracticeDocumentService
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public PracticeDocumentService(
            IDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IUserService userService,
            IOptionsSnapshot<AppConfiguration> settings = null)
            : base(repository, unitOfWork, settings)
        {
            _documentType = DocumentTypesEnum.PracticeDocument;
            PointsForDocumentFree = Constants.PointsFor.DOCUMENT.PRACTICE.FREE;
            PointsForDocumentPaid = Constants.PointsFor.DOCUMENT.PRACTICE.PAID;
            PointsForDocumentFreeView = Constants.PointsFor.DOCUMENT.PRACTICE.FREE_VIEW;
            PointsForDocumentPurchase = Constants.PointsFor.DOCUMENT.PRACTICE.PURCHASE;
            PointsForDocumentFreeLike = Constants.PointsFor.DOCUMENT.PRACTICE.FREE_LIKE;
            PointsForDocumentPaidLike = Constants.PointsFor.DOCUMENT.PRACTICE.PAID_LIKE;

            _emailService = emailService;
            _userService = userService;
        }

        public async override void SubmitForApproval(Document entity, string userID)
        {
            base.SubmitForApproval(entity, userID);

            try
            {
                List<string> adminEmails = _userService.GetAdminEmails();
                List<Tuple<string, string>> adminEmailsWithNames = new List<Tuple<string, string>>();
                foreach (var item in adminEmails)
                {
                    adminEmailsWithNames.Add(new Tuple<string, string>(item, "Админ"));
                }

                string subjectUz = "Янги амалиёт";
                string subjectRu = "Новый практический документ";
                if (IsFree(entity))
                {
                    subjectUz = "Янги пуллик амалиёт";
                    subjectRu = "Новый платный практический документ";
                }

                string documentUrl = "/Admin/PracticeDocument/Details/" + entity.ID;
                string messageUz = "Янги амалиёт тасдиқлашга жўнатилинди. <a href='" + documentUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Поступил новый практический документ на подтверждение. <a href='" + documentUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    adminEmailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }
    }

    //public class QuestionDocumentService : DocumentService, IQuestionDocumentService
    //{

    //    public QuestionDocumentService(IDocumentRepository repository,
    //                       IUnitOfWork unitOfWork)
    //        : base(repository, unitOfWork)
    //    {
    //        _documentType = DocumentTypesEnum.QuestionDocument;
    //    }

    //    public void CompleteWithDistribution(Document question, string userID)
    //    {
    //        question.QuestionFeeIsDistributed = true;
    //        Complete(question, userID);
    //    }

    //    public void Complete(Document question, string userID)
    //    {
    //        question.QuestionIsCompleted = true;
    //        question.DateQuestionCompleted = DateTime.Now;

    //        Update(question, userID);
    //    }

    //    public bool AdminIsAllowedToComplete(Document question)
    //    {
    //        var now = DateTime.Now;

    //        if (!question.DateApproved.HasValue)
    //            return false;

    //        if (question.DateApproved.Value.AddDays(1) > now)
    //        //if (question.DateApproved.Value.AddMinutes(10) > now)
    //            return true;

    //        return false;
    //    }

    //    public bool IsCompleted(Document question)
    //    {
    //        return question.QuestionIsCompleted.HasValue ? 
    //            question.QuestionIsCompleted.Value : false;
    //    }
    //}
}