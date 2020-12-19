using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Linq;

namespace Expro.Services
{
    public class ClickTransactionService : BaseCRUDService<ClickTransaction>, IClickTransactionService
    {
        public ClickTransactionService(IClickTransactionRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public void MarkTransactionAsCanceled(ClickTransaction transaction)
        {
            transaction.StatusID = (int)ClickTransactionStatusEnum.CANCELLED;
            transaction.DateCancelled = DateTime.Now;

            Update(transaction);
        }

        public void MarkTransactionAsPaid(ClickTransaction transaction)
        {
            transaction.StatusID = (int)ClickTransactionStatusEnum.SUCCESS;
            transaction.DateSuccess = DateTime.Now;

            Update(transaction);
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