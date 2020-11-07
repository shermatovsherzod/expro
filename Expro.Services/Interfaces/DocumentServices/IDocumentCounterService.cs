using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IDocumentCounterService
    {
        void IncrementNumberOfViews(Document model);
        void IncrementNumberOfPurchases(Document model);
    }
}