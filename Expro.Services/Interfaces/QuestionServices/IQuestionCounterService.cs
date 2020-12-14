using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IQuestionCounterService
    {
        void IncrementNumberOfViews(Question model);
        void IncrementNumberOfPurchases(Question model);
    }
}