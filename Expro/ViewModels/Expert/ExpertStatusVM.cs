using Expro.Common.Utilities;
using Expro.Models.Enums;
using Expro.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Expro.Models
{
    public class ExpertStatusVM
    {
        public ExpertStatusVM()
        {

        }

        [Display(Name = "Статус")]
        public int ApproveStatus { get; set; }

        [Display(Name = "Дата подтверждения статуса")]
        public DateTime? DateApproved { get; set; }

        [Display(Name = "Дата отклонения статуса")]
        public DateTime? DateRejected { get; set; }

        [Display(Name = "Дата отправления статуса на подтверждение")]
        public DateTime? DateSubmittedForApproval { get; set; }

        public ExpertStatusVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;
                      
            ApproveStatus = model.ApproveStatus;
            DateApproved = model.DateApproved;
            DateRejected = model.DateRejected;
            DateSubmittedForApproval = model.DateSubmittedForApproval;
        }
    }
}
