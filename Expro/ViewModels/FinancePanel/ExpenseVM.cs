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
    public class ExpenseVM
    {
        [Display(Name = "Дата и время")]
        public string DateTimeStr { get; set; }

        [Display(Name = "На что")]
        public string PurchaseObject { get; set; }

        [Display(Name = "Кому")]
        public AppUserVM PurchaseObjectAuthor { get; set; }

        [Display(Name = "Сумма")]
        public int Amount { get; set; }
        public string AmountStr { get; set; }

        public ExpenseVM() { }

        public ExpenseVM(UserPurchasedDocument documentPurchased)
        {
            if (documentPurchased == null)
                return;

            DateTimeStr = DateTimeUtils.ConvertToString(documentPurchased.DatePurchased, AppData.Configuration.DateTimeViewStringFormat);
            PurchaseObject = documentPurchased.Document.DocumentType.Name;
            PurchaseObjectAuthor = new AppUserVM(documentPurchased.Document.Creator);
            Amount = (int)documentPurchased.Price;
            AmountStr = Amount.ToString(AppData.Configuration.NumberViewStringFormat);
        }

        public ExpenseVM(Question question)
        {
            if (question == null)
                return;

            if (question.DateQuestionCompleted.HasValue && question.QuestionFeeIsDistributed == true)
                DateTimeStr = DateTimeUtils.ConvertToString(question.DateQuestionCompleted.Value, AppData.Configuration.DateTimeViewStringFormat);

            PurchaseObject = "Савол";

            if (question.Price.HasValue)
            {
                Amount = question.Price.Value;
                AmountStr = Amount.ToString(AppData.Configuration.NumberViewStringFormat);
            }
        }
    }
}
