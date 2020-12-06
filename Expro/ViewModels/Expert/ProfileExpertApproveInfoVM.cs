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
    public class ProfileExpertApproveInfoVM
    {
        public ProfileExpertApproveInfoVM()
        {

        }
        public string Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(256)]
        public string LastName { get; set; }

        [Display(Name = "На сайте с:")]
        public string DateRegistered { get; set; }

        [Display(Name = "Статус")]
        public int UserStatusID { get; set; }

        [Display(Name = "Дата подтверждения статуса")]
        public DateTime? DateApproved { get; set; }

        [Display(Name = "Дата отклонения статуса")]
        public DateTime? DateRejected { get; set; }

        [Display(Name = "Дата отправления статуса на подтверждение")]
        public DateTime? DateSubmittedForApproval { get; set; }

        public ProfileExpertApproveInfoVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;          
            DateRegistered = DateTimeUtils.ConvertToString(model.DateRegistered);
            UserStatusID = model.UserStatusID;
            DateApproved = model.DateApproved;
            DateRejected = model.DateRejected;
            DateSubmittedForApproval = model.DateSubmittedForApproval;
        }
    }
}
