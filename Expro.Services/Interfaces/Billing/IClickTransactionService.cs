using Expro.Models;

namespace Expro.Services.Interfaces
{
    public interface IClickTransactionService : IBaseCRUDService<ClickTransaction>
    {
        void MarkTransactionAsCanceled(ClickTransaction transaction);
        void MarkTransactionAsPaid(ClickTransaction transaction);
        bool IsTransactionPaid(ClickTransaction transaction);
        bool IsTransactionCancelled(ClickTransaction transaction);
    }
}