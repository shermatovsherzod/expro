using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IClickTransactionService : IBaseCRUDService<ClickTransaction>
    {
        void MarkTransactionAsCanceled(ClickTransaction transaction);
        void MarkTransactionAsPaid(ClickTransaction transaction);
        bool IsTransactionPaid(ClickTransaction transaction);
        bool IsTransactionCancelled(ClickTransaction transaction);
        IQueryable<ClickTransaction> GetAllByCreator(string userID);
    }
}