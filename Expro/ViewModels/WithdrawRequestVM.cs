using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Expro.ViewModels
{
    public class WithdrawRequestCreateVM// : BaseVM
    {
        [Display(Name = "Сумма")]
        public int Amount { get; set; }

        public WithdrawRequestCreateVM() { }

        //public WithdrawRequestCreateVM(Document model)
        //    : base(model)
        //{
        //    if (model == null)
        //        return;

        //    Title = model.Title;
        //}

        public virtual WithdrawRequest ToModel()
        {
            WithdrawRequest model = new WithdrawRequest()
            {
                Amount = this.Amount,
                //StatusID = (int)WithdrawRequestStatusesEnum.Pending
            };

            return model;
        }
    }

    public class WithdrawRequestListItemForExpertVM : BaseVM
    {
        [Display(Name = "Сумма")]
        public int Amount { get; set; }

        [Display(Name = "Сумма")]
        public string AmountStr { get; set; }

        [Display(Name = "Статус")]
        public BaseDropdownableDetailsVM Status { get; set; }

        [Display(Name = "Дата создания")]
        public string DateCreated { get; set; }

        [Display(Name = "Дата выполнения")]
        public string DateCompleted { get; set; }

        public WithdrawRequestListItemForExpertVM() { }

        public WithdrawRequestListItemForExpertVM(WithdrawRequest model)
            : base(model)
        {
            if (model == null)
                return;

            Amount = model.Amount;
            AmountStr = model.Amount.ToString(AppData.Configuration.NumberViewStringFormat);

            Status = new BaseDropdownableDetailsVM(model.Status);
            DateCreated = DateTimeUtils.ConvertToString(model.DateCreated);
            DateCompleted = DateTimeUtils.ConvertToString(model.DateCompleted);
        }
    }
}
