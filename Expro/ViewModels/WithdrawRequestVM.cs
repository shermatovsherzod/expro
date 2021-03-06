﻿using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using System.ComponentModel.DataAnnotations;

namespace Expro.ViewModels
{
    public class WithdrawRequestCreateVM// : BaseVM
    {
        [Display(Name = "Amount", ResourceType = typeof(Resources.ResourceTexts))]
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

    public class WithdrawRequestListItemForExpertAndUserVM : BaseVM
    {
        [Display(Name = "Amount", ResourceType = typeof(Resources.ResourceTexts))]
        public int Amount { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.ResourceTexts))]
        public string AmountStr { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resources.ResourceTexts))]
        public BaseDropdownableDetailsVM Status { get; set; }

        [Display(Name = "DateCreated", ResourceType = typeof(Resources.ResourceTexts))]
        public string DateCreated { get; set; }

        [Display(Name = "DateCompleted", ResourceType = typeof(Resources.ResourceTexts))]
        public string DateCompleted { get; set; }

        public WithdrawRequestListItemForExpertAndUserVM() { }

        public WithdrawRequestListItemForExpertAndUserVM(WithdrawRequest model)
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

    public class WithdrawRequestListItemForAdminVM : WithdrawRequestListItemForExpertAndUserVM
    {
        public AppUserVM User { get; set; }

        public WithdrawRequestListItemForAdminVM() { }

        public WithdrawRequestListItemForAdminVM(WithdrawRequest model)
            : base(model)
        {
            User = new AppUserVM(model.Creator);
        }
    }
}
