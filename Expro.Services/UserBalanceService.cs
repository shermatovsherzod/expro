using Expro.Common;
using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class UserBalanceService : IUserBalanceService
    {
        public int GetBalance(ApplicationUser user)
        {
            return user?.Balance ?? 0;
        }

        public void ReplenishBalance(ApplicationUser user, int amount)
        {
            user.Balance += amount;
        }

        public void TakeOffBalance(ApplicationUser user, int amount)
        {
            user.Balance -= amount;
        }

        public string GenerateClickPaymentButtonUrl(string accountNumber, int amount, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new Exception("accountNumber is not provided");

            if (amount <= 0)
                throw new Exception("amount must be positive integer");

            string url = @"https://my.click.uz/services/pay";
            url += "?service_id=" + Constants.CLICK.SETTINGS.SERVICE_id;
            url += "&merchant_id=" + Constants.CLICK.SETTINGS.MERCHANT_ID;
            url += "&amount=" + amount + ".00";
            url += "&transaction_param=" + accountNumber;

            if (!string.IsNullOrWhiteSpace(returnUrl))
                url += "&return_url=" + returnUrl;

            return url;
        }
    }
}