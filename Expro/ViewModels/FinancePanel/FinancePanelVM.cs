using Expro.Common;
using Expro.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class FinancePanelVM
    {
        public AppUserVM User { get; set; }
        public string UserFullName { get;set; }

        [Display(Name = "Balance", ResourceType = typeof(Resources.ResourceTexts))]
        public int Balance { get; set; }
        public string BalanceStr { get; set; }

        [Display(Name = "Expenses", ResourceType = typeof(Resources.ResourceTexts))]
        public List<ExpenseVM> Expenses { get; set; }

        [Display(Name = "Withdrawals", ResourceType = typeof(Resources.ResourceTexts))]
        public List<WithdrawalVM> Withdrawals { get; set; }

        public FinancePanelVM() { }

        public FinancePanelVM(
            ApplicationUser user,
            int balance,
            List<UserPurchasedDocument> documentsPurchased,
            List<Question> questions,
            List<WithdrawRequest> withdrawRequests,
            IStringLocalizer<Resources.ResourceTexts> localizer)
        {
            User = new AppUserVM(user);
            Balance = balance;
            BalanceStr = Balance.ToString(AppData.Configuration.NumberViewStringFormat);

            Expenses = new List<ExpenseVM>();
            if (documentsPurchased != null && documentsPurchased.Count > 0)
            {
                foreach (var item in documentsPurchased)
                {
                    Expenses.Add(new ExpenseVM(item, localizer));
                }
            }
            
            if (questions != null && questions.Count > 0)
            {
                foreach (var item in questions)
                {
                    Expenses.Add(new ExpenseVM(item, localizer));
                }
            }

            Withdrawals = new List<WithdrawalVM>();
            if (withdrawRequests != null && withdrawRequests.Count > 0)
            {
                foreach (var item in withdrawRequests)
                {
                    Withdrawals.Add(new WithdrawalVM(item));
                }
            }
        }
    }

    public class UserFinancePanelVM : FinancePanelVM
    {
        [Display(Name = "Transactions", ResourceType = typeof(Resources.ResourceTexts))]
        public List<TransactionVM> Transactions { get; set; }

        public UserFinancePanelVM() { }
        public UserFinancePanelVM(
            ApplicationUser user,
            int balance, 
            List<UserPurchasedDocument> documentsPurchased,
            List<Question> questions,
            List<WithdrawRequest> withdrawRequests,
            List<ClickTransaction> clickTransactions,
            IStringLocalizer<Resources.ResourceTexts> localizer)
            : base(
                  user,
                  balance,
                  documentsPurchased,
                  questions,
                  withdrawRequests,
                  localizer)
        {
            Transactions = new List<TransactionVM>();
            if (clickTransactions != null && clickTransactions.Count > 0)
            {
                foreach (var item in clickTransactions)
                {
                    Transactions.Add(new TransactionVM(item));
                }
            }
        }
    }

    public class ExpertFinancePanelVM : FinancePanelVM
    {
        [Display(Name = "Incomes", ResourceType = typeof(Resources.ResourceTexts))]
        public List<IncomeVM> Incomes { get; set; }

        public ExpertFinancePanelVM() { }
        public ExpertFinancePanelVM(
            ApplicationUser user,
            int balance,
            List<UserPurchasedDocument> documentsPurchased,
            List<Question> questions,
            List<WithdrawRequest> withdrawRequests,
            List<UserPurchasedDocument> documentsSold,
            List<QuestionAnswer> questionAnswers,
            IStringLocalizer<Resources.ResourceTexts> localizer)
            : base(
                  user,
                  balance,
                  documentsPurchased,
                  questions,
                  withdrawRequests,
                  localizer)
        {
            Incomes = new List<IncomeVM>();
            if (documentsSold != null && documentsSold.Count > 0)
            {
                foreach (var item in documentsSold)
                {
                    Incomes.Add(new IncomeVM(item));
                }
            }
            
            if (questionAnswers != null && questionAnswers.Count > 0)
            {
                foreach (var item in questionAnswers)
                {
                    Incomes.Add(new IncomeVM(item, localizer));
                }
            }
        }
    }
}
