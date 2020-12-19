using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class TransactionVM
    {
        [Display(Name = "Дата и время")]
        public string DateTimeStr { get; set; }

        [Display(Name = "Провайдер")]
        public string Provider { get; set; }

        [Display(Name = "Сумма")]
        public int Amount { get; set; }
        public string AmountStr { get; set; }

        public TransactionVM() { }

        public TransactionVM(ClickTransaction transaction)
        {
            if (transaction == null)
                return;

            if (transaction.DateSuccess.HasValue)
                DateTimeStr = DateTimeUtils.ConvertToString(transaction.DateSuccess.Value, AppData.Configuration.DateTimeViewStringFormat);

            Provider = "Click";
            Amount = (int)transaction.Amount;
            AmountStr = Amount.ToString(AppData.Configuration.NumberViewStringFormat);
        }
    }
}
