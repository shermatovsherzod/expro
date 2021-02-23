using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class IncomeVM
    {
        [Display(Name = "DateTime", ResourceType = typeof(Resources.ResourceTexts))]
        public string DateTimeStr { get; set; }

        [Display(Name = "PurchaseObject", ResourceType = typeof(Resources.ResourceTexts))]
        public string PurchaseObject { get; set; }

        [Display(Name = "PurchaseObjectPurchaser", ResourceType = typeof(Resources.ResourceTexts))]
        public AppUserVM PurchaseObjectPurchaser { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.ResourceTexts))]
        public int Amount { get; set; }
        public string AmountStr { get; set; }

        public IncomeVM() { }

        public IncomeVM(UserPurchasedDocument documentPurchased)
        {
            if (documentPurchased == null)
                return;

            DateTimeStr = DateTimeUtils.ConvertToString(documentPurchased.DatePurchased, AppData.Configuration.DateTimeViewStringFormat);
            PurchaseObject = documentPurchased.Document.DocumentType.Name;
            PurchaseObjectPurchaser = new AppUserVM(documentPurchased.User);
            Amount = (int)documentPurchased.Price;
            AmountStr = Amount.ToString(AppData.Configuration.NumberViewStringFormat);
        }

        public IncomeVM(
            QuestionAnswer questionAnswer,
            IStringLocalizer<Resources.ResourceTexts> localizer)
        {
            if (questionAnswer == null)
                return;

            DateTimeStr = DateTimeUtils.ConvertToString(questionAnswer.DateCreated, AppData.Configuration.DateTimeViewStringFormat);
            PurchaseObject = localizer["Question"];
            PurchaseObjectPurchaser = new AppUserVM(questionAnswer.Question.Creator);
            if (questionAnswer.PaidFee.HasValue)
            {
                Amount = questionAnswer.PaidFee.Value;
                AmountStr = Amount.ToString(AppData.Configuration.NumberViewStringFormat);
            }
        }
    }
}
