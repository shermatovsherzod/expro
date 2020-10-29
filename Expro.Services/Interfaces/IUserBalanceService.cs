using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Expro.Services.Interfaces
{
    public interface IUserBalanceService
    {
        int GetBalance(ApplicationUser user);
        string GenerateClickPaymentButtonUrl(string accountNumber, int amount, string returnUrl);
    }
}