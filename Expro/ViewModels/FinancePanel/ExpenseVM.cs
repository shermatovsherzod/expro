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
    public class ExpenseVM
    {
        [Display(Name = "DateTime", ResourceType = typeof(Resources.ResourceTexts))]
        public string DateTimeStr { get; set; }

        [Display(Name = "PurchaseObject", ResourceType = typeof(Resources.ResourceTexts))]
        public string PurchaseObject { get; set; }

        [Display(Name = "PurchaseObjectAuthor", ResourceType = typeof(Resources.ResourceTexts))]
        public AppUserVM PurchaseObjectAuthor { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.ResourceTexts))]
        public int Amount { get; set; }
        public string AmountStr { get; set; }

        public ExpenseVM() { }

        public ExpenseVM(
            UserPurchasedDocument documentPurchased,
            IStringLocalizer<Resources.ResourceTexts> localizer)
        {
            if (documentPurchased == null)
                return;

            DateTimeStr = DateTimeUtils.ConvertToString(documentPurchased.DatePurchased, AppData.Configuration.DateTimeViewStringFormat);
            PurchaseObject = documentPurchased.Document.DocumentType.Name;
            PurchaseObjectAuthor = new AppUserVM(documentPurchased.Document.Creator);
            Amount = (int)documentPurchased.Price;
            AmountStr = Amount.ToString(AppData.Configuration.NumberViewStringFormat);
        }

        public ExpenseVM(
            Question question,
            IStringLocalizer<Resources.ResourceTexts> localizer)
        {
            if (question == null)
                return;

            if (question.DateQuestionCompleted.HasValue && question.QuestionFeeIsDistributed == true)
                DateTimeStr = DateTimeUtils.ConvertToString(question.DateQuestionCompleted.Value, AppData.Configuration.DateTimeViewStringFormat);

            PurchaseObject = localizer["Question"];

            if (question.Price.HasValue)
            {
                Amount = question.Price.Value;
                AmountStr = Amount.ToString(AppData.Configuration.NumberViewStringFormat);
            }
        }
    }
}
