using Expro.Models;
using Expro.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels.Expert
{
    public class ExpertProfileContactVM
    {
        public ExpertProfileContactVM() { }

        //[Required]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        
        [Display(Name = "Факс")]
        public string Fax { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "Веб сайт")]
        public string WebSite { get; set; }


        public ExpertProfileContactVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;

           // Email = model.Email;
            PhoneNumber = model.PhoneNumber;
            Fax = model.Fax;
            WebSite = model.WebSite;
        }
    }
}
