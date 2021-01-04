using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using Expro.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Expro.ViewModels
{
    public class ExpertStatusVM
    {
        public ExpertStatusVM()
        {

        }

        [Display(Name = "Статус")]
        public int UserStatusID { get; set; }
        public string UserStatusName { get; set; }
        
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

            UserStatusID = model.UserStatusID;
            DateApproved = model.DateApproved;
            DateRejected = model.DateRejected;
            DateSubmittedForApproval = model.DateSubmittedForApproval;
            UserStatusName = model.UserStatus.Name;           
        }
    }
}
