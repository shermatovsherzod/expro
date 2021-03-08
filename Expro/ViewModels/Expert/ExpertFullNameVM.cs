using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Resources;
using Expro.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Expro.ViewModels
{
    public class ExpertFullNameVM
    {
        public ExpertFullNameVM()
        {

        }
        public string Id { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "Имя")]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "Фамилия")]
        [StringLength(256)]
        public string LastName { get; set; }


        public ExpertFullNameVM(ApplicationUser model)
        {
            if (model == null)
                return;
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
        }
    }
}
