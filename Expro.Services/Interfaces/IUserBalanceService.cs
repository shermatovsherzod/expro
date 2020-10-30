using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Expro.Services.Interfaces
{
    public interface IUserBalanceService
    {
        int GetBalance(ApplicationUser user);
        void ReplenishBalance(ApplicationUser user, int amount);
        void TakeOffBalance(ApplicationUser user, int amount);
        string GenerateClickPaymentButtonUrl(string accountNumber, int amount, string returnUrl);
    }
}