using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Expro.Services
{
    public class ArticleDocumentAdminActionsService : DocumentAdminActionsService, IArticleDocumentAdminActionsService
    {
        private readonly IEmailService _emailService;

        public ArticleDocumentAdminActionsService(
            IDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
            : base(repository, unitOfWork)
        {
            _emailService = emailService;
        }

        public async override void Approve(Document entity, string userID)
        {
            base.Approve(entity, userID);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = entity.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Мақола администратор томонидан тасдиқланди";
                string subjectRu = "Статья подтверждена администратором";

                string documentUrl = "/ArticleDocument/Details/" + entity.ID;
                string messageUz = "Мақола администратор томонидан тасдиқланди. <a href='" + documentUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Статья подтверждена администратором. <a href='" + documentUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }

        public async override void Reject(Document entity, string userID)
        {
            base.Reject(entity, userID);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = entity.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Мақола администратор томонидан рад этилди";
                string subjectRu = "Статья отменена администратором";

                string documentUrl = "/Expert/ArticleDocument/Details/" + entity.ID;
                string messageUz = "Мақола администратор томонидан рад этилди. <a href='" + documentUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Статья отменена администратором. <a href='" + documentUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }
    }

    public class SampleDocumentAdminActionsService : DocumentAdminActionsService, ISampleDocumentAdminActionsService
    {
        private readonly IEmailService _emailService;

        public SampleDocumentAdminActionsService(
            IDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
            : base(repository, unitOfWork)
        {
            _emailService = emailService;
        }

        public async override void Approve(Document entity, string userID)
        {
            base.Approve(entity, userID);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = entity.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Намунавий хужжат администратор томонидан тасдиқланди";
                string subjectRu = "Образцовый документ подтвержден администратором";

                string documentUrl = "/SampleDocument/Details/" + entity.ID;
                string messageUz = "Намунавий хужжат администратор томонидан тасдиқланди. <a href='" + documentUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Образцовый документ подтвержден администратором. <a href='" + documentUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }

        public async override void Reject(Document entity, string userID)
        {
            base.Reject(entity, userID);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = entity.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Намунавий хужжат администратор томонидан рад этилди";
                string subjectRu = "Образцовый документ отменен администратором";

                string documentUrl = "/Expert/SampleDocument/Details/" + entity.ID;
                string messageUz = "Намунавий хужжат администратор томонидан рад этилди. <a href='" + documentUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Образцовый документ отменен администратором. <a href='" + documentUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }
    }

    public class PracticeDocumentAdminActionsService : DocumentAdminActionsService, IPracticeDocumentAdminActionsService
    {
        private readonly IEmailService _emailService;

        public PracticeDocumentAdminActionsService(
            IDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
            : base(repository, unitOfWork)
        {
            _emailService = emailService;
        }

        public async override void Approve(Document entity, string userID)
        {
            base.Approve(entity, userID);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = entity.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Амалиёт администратор томонидан тасдиқланди";
                string subjectRu = "Практический документ подтвержден администратором";

                string documentUrl = "/PracticeDocument/Details/" + entity.ID;
                string messageUz = "Амалиёт администратор томонидан тасдиқланди. <a href='" + documentUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Практический документ подтвержден администратором. <a href='" + documentUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }

        public async override void Reject(Document entity, string userID)
        {
            base.Reject(entity, userID);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var creator = entity.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Амалиёт администратор томонидан рад этилди";
                string subjectRu = "Практический документ отменен администратором";

                string documentUrl = "/Expert/PracticeDocument/Details/" + entity.ID;
                string messageUz = "Амалиёт администратор томонидан рад этилди. <a href='" + documentUrl + "'>" + entity.Title + "</a>";
                string messageRu = "Практический документ отменен администратором. <a href='" + documentUrl + "'>" + entity.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }
    }
}