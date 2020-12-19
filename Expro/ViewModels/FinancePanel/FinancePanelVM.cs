using Expro.Common;
using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class FinancePanelVM
    {
        [Display(Name = "Баланс")]
        public int Balance { get; set; }
        public string BalanceStr { get; set; }

        [Display(Name = "Расходы")]
        public List<ExpenseVM> Expenses { get; set; }

        [Display(Name = "Выводы средств")]
        public List<WithdrawalVM> Withdrawals { get; set; }

        public FinancePanelVM() { }

        public FinancePanelVM(
            int balance,
            List<UserPurchasedDocument> documentsPurchased,
            List<Question> questions,
            List<WithdrawRequest> withdrawRequests)
        {
            Balance = balance;
            BalanceStr = Balance.ToString(AppData.Configuration.NumberViewStringFormat);

            Expenses = new List<ExpenseVM>();
            if (documentsPurchased != null && documentsPurchased.Count > 0)
            {
                foreach (var item in documentsPurchased)
                {
                    Expenses.Add(new ExpenseVM(item));
                }
            }
            
            if (questions != null && questions.Count > 0)
            {
                foreach (var item in questions)
                {
                    Expenses.Add(new ExpenseVM(item));
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
        [Display(Name = "Пополнения")]
        public List<TransactionVM> Transactions { get; set; }

        public UserFinancePanelVM() { }
        public UserFinancePanelVM(
            int balance, 
            List<UserPurchasedDocument> documentsPurchased,
            List<Question> questions,
            List<WithdrawRequest> withdrawRequests,
            List<ClickTransaction> clickTransactions)
            : base(
                  balance,
                  documentsPurchased,
                  questions,
                  withdrawRequests)
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
        [Display(Name = "Заработки")]
        public List<IncomeVM> Incomes { get; set; }

        public ExpertFinancePanelVM() { }
        public ExpertFinancePanelVM(
            int balance,
            List<UserPurchasedDocument> documentsPurchased,
            List<Question> questions,
            List<WithdrawRequest> withdrawRequests,
            List<UserPurchasedDocument> documentsSold,
            List<QuestionAnswer> questionAnswers)
            : base(
                  balance,
                  documentsPurchased,
                  questions,
                  withdrawRequests)
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
                    Incomes.Add(new IncomeVM(item));
                }
            }
        }
    }
}
