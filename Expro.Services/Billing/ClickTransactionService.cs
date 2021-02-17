using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class ClickTransactionService : BaseCRUDService<ClickTransaction>, IClickTransactionService
    {
        private readonly IEmailService _emailService;

        public ClickTransactionService(
            IClickTransactionRepository repository,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
            : base(repository, unitOfWork)
        {
            _emailService = emailService;
        }

        public void MarkTransactionAsCanceled(ClickTransaction transaction)
        {
            transaction.StatusID = (int)ClickTransactionStatusEnum.CANCELLED;
            transaction.DateCancelled = DateTime.Now;

            Update(transaction);
        }

        public async void MarkTransactionAsPaid(ClickTransaction transaction)
        {
            transaction.StatusID = (int)ClickTransactionStatusEnum.SUCCESS;
            transaction.DateSuccess = DateTime.Now;

            Update(transaction);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var user = transaction.User;
                emailsWithNames.Add(new Tuple<string, string>(user.Email, user.FirstName + " " + user.LastName));

                string subjectUz = "Баланс тўлдирилди";
                string subjectRu = "Пополнение баланса";

                int amount = (int)transaction.Amount;
                string messageUz = "Балансингиз " + amount + " сўмга тўлдирилди. Баланс: " + user.Balance + " сўм.";
                string messageRu = "Ваш баланс пополнен на " + amount + " сум. Баланс: " + user.Balance + " сум.";

                await _emailService.SendEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }

        public bool IsTransactionPaid(ClickTransaction transaction)
        {
            return transaction.StatusID == (int)ClickTransactionStatusEnum.SUCCESS;
        }

        public bool IsTransactionCancelled(ClickTransaction transaction)
        {
            return transaction.StatusID == (int)ClickTransactionStatusEnum.CANCELLED;
        }

        public IQueryable<ClickTransaction> GetAllByCreator(string userID)
        {
            return GetAsIQueryable()
                .Where(m => m.UserID == userID);
        }
    }
}