using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using Expro.Models.Enums;

namespace Expro.Services
{
    public class UserPurchasedDocumentService : BaseCRUDService<UserPurchasedDocument>, IUserPurchasedDocumentService
    {
        private readonly IUserBalanceService UserBalanceService;
        private readonly IUserPurchasedDocumentRepository _userPurchasedDocumentRepository;

        public UserPurchasedDocumentService(
            IUserPurchasedDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IUserBalanceService userBalanceService)
            : base(repository, unitOfWork)
        {
            UserBalanceService = userBalanceService;
            _userPurchasedDocumentRepository = repository;
        }

        public virtual void Purchase(ApplicationUser user, Document document)
        {
            if (UserPurchasedDocumentBefore(user, document))
                throw new Exception("Вы уже покупали этот документ");

            var model = new UserPurchasedDocument()
            {
                User = user,
                Document = document,
                Price = document.Price ?? 0,
                DatePurchased = DateTime.Now
            };

            UserBalanceService.TakeOffBalance(user, model.Price);
            UserBalanceService.ReplenishBalance(document.Creator, model.Price);

            Add(model);
        }

        public bool UserPurchasedDocumentBefore(ApplicationUser user, Document document)
        {
            return GetAsIQueryable()
                .FirstOrDefault(m => m.DocumentID == document.ID && m.UserID == user.Id) != null;
        }

        public IQueryable<UserPurchasedDocument> GetPurchasesByUser(string userID)
        {
            return _userPurchasedDocumentRepository
                .GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.UserID == userID);
        }

        public IQueryable<UserPurchasedDocument> GetSalesByUser(string userID)
        {
            return _userPurchasedDocumentRepository
                .GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.Document.CreatedBy == userID);
        }
    }

    public class UserPurchasedArticleDocumentService : UserPurchasedDocumentService, IUserPurchasedArticleDocumentService
    {
        private readonly IEmailService _emailService;

        public UserPurchasedArticleDocumentService(
            IUserPurchasedDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IUserBalanceService userBalanceService,
            IEmailService emailService)
            : base(repository, unitOfWork, userBalanceService)
        {
            _emailService = emailService;
        }

        public async override void Purchase(ApplicationUser user, Document document)
        {
            base.Purchase(user, document);

            try
            {
                //to purchaser
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                //var creator = document.Creator;
                emailsWithNames.Add(new Tuple<string, string>(user.Email, user.FirstName + " " + user.LastName));

                string subjectUz = "Мақола сотиб олинди";
                string subjectRu = "Статья куплена";

                string documentUrl = (user.UserType == UserTypesEnum.SimpleUser) ? "/User" : "/Expert";
                documentUrl += "https://expro.uz/ArticleDocumentPurchased/Details/" + document.ID;
                string messageUz = "Мақолани сотиб олганингиз учут рахмат <a href='" + documentUrl + "'>" + document.Title + "</a>";
                string messageRu = "Спасибо за покупку статьи <a href='" + documentUrl + "'>" + document.Title + "</a>";

                _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);

                //to document author
                emailsWithNames.Clear();
                var creator = document.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                subjectUz = "Мақолангизни сотиб олишди";
                subjectRu = "Вашу статью купили";

                documentUrl = "https://expro.uz/ArticleDocument/Details/" + document.ID;
                messageUz = "Сизнинг мақолангизни сотиб олишди <a href='" + documentUrl + "'>" + document.Title + "</a>";
                messageRu = "Вашу статью купили <a href='" + documentUrl + "'>" + document.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }
    }

    public class UserPurchasedSampleDocumentService : UserPurchasedDocumentService, IUserPurchasedSampleDocumentService
    {
        private readonly IEmailService _emailService;

        public UserPurchasedSampleDocumentService(
            IUserPurchasedDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IUserBalanceService userBalanceService,
            IEmailService emailService)
            : base(repository, unitOfWork, userBalanceService)
        {
            _emailService = emailService;
        }

        public async override void Purchase(ApplicationUser user, Document document)
        {
            base.Purchase(user, document);

            try
            {
                //to purchaser
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                //var creator = document.Creator;
                emailsWithNames.Add(new Tuple<string, string>(user.Email, user.FirstName + " " + user.LastName));

                string subjectUz = "Намунавий хужжат сотиб олинди";
                string subjectRu = "Образцовый документ куплен";

                string documentUrl = (user.UserType == UserTypesEnum.SimpleUser) ? "/User" : "/Expert";
                documentUrl += "https://expro.uz/SampleDocumentPurchased/Details/" + document.ID;
                string messageUz = "Намунавий хужжатни сотиб олганингиз учут рахмат <a href='" + documentUrl + "'>" + document.Title + "</a>";
                string messageRu = "Спасибо за покупку образцового документа <a href='" + documentUrl + "'>" + document.Title + "</a>";

                _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);

                //to document author
                emailsWithNames.Clear();
                var creator = document.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                subjectUz = "Намунавий хужжатингизни сотиб олишди";
                subjectRu = "Ваш образцовый документ купили";

                documentUrl = "https://expro.uz/SampleDocument/Details/" + document.ID;
                messageUz = "Сизнинг намунавий хужжатингизни сотиб олишди <a href='" + documentUrl + "'>" + document.Title + "</a>";
                messageRu = "Ваш образцовый документ купили <a href='" + documentUrl + "'>" + document.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }
    }

    public class UserPurchasedPracticeDocumentService : UserPurchasedDocumentService, IUserPurchasedPracticeDocumentService
    {
        private readonly IEmailService _emailService;

        public UserPurchasedPracticeDocumentService(
            IUserPurchasedDocumentRepository repository,
            IUnitOfWork unitOfWork,
            IUserBalanceService userBalanceService,
            IEmailService emailService)
            : base(repository, unitOfWork, userBalanceService)
        {
            _emailService = emailService;
        }

        public async override void Purchase(ApplicationUser user, Document document)
        {
            base.Purchase(user, document);

            try
            {
                //to purchaser
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                //var creator = document.Creator;
                emailsWithNames.Add(new Tuple<string, string>(user.Email, user.FirstName + " " + user.LastName));

                string subjectUz = "Амалиёт сотиб олинди";
                string subjectRu = "Практический документ куплена";

                string documentUrl = (user.UserType == UserTypesEnum.SimpleUser) ? "/User" : "/Expert";
                documentUrl += "https://expro.uz/PracticeDocumentPurchased/Details/" + document.ID;
                string messageUz = "Амалиётни сотиб олганингиз учут рахмат <a href='" + documentUrl + "'>" + document.Title + "</a>";
                string messageRu = "Спасибо за покупку статьи <a href='" + documentUrl + "'>" + document.Title + "</a>";

                _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);

                //to document author
                emailsWithNames.Clear();
                var creator = document.Creator;
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                subjectUz = "Амалиётингизни сотиб олишди";
                subjectRu = "Ваш практический документ купили";

                documentUrl = "https://expro.uz/PracticeDocument/Details/" + document.ID;
                messageUz = "Сизнинг амалиётингизни сотиб олишди <a href='" + documentUrl + "'>" + document.Title + "</a>";
                messageRu = "Ваш практический документ купили <a href='" + documentUrl + "'>" + document.Title + "</a>";

                await _emailService.SendAutomaticallyGeneratedEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }
    }
}