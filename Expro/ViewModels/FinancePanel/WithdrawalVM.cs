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
    public class WithdrawalVM
    {
        [Display(Name = "DateTime", ResourceType = typeof(Resources.ResourceTexts))]
        public string DateTimeStr { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resources.ResourceTexts))]
        public BaseDropdownableDetailsVM Status { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.ResourceTexts))]
        public int Amount { get; set; }
        public string AmountStr { get; set; }

        public WithdrawalVM() { }

        public WithdrawalVM(WithdrawRequest withdrawRequest)
        {
            if (withdrawRequest == null)
                return;

            DateTimeStr = DateTimeUtils.ConvertToString(withdrawRequest.DateCreated, AppData.Configuration.DateTimeViewStringFormat);
            Status = new BaseDropdownableDetailsVM(withdrawRequest.Status);
            Amount = withdrawRequest.Amount;
            AmountStr = Amount.ToString(AppData.Configuration.NumberViewStringFormat);
        }
    }
}
