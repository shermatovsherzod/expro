using Expro.Models;
using Expro.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class ExpertProfileContactEditVM
    {
        public ExpertProfileContactEditVM() { }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblPhoneNumber", ResourceType = typeof(ResourceTexts))]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "lblFax", ResourceType = typeof(ResourceTexts))]
        public string Fax { get; set; }

        [Display(Name = "lblWebSite", ResourceType = typeof(ResourceTexts))]
        public string WebSite { get; set; }

        public ExpertProfileContactEditVM(ApplicationUser model) 
        {
            if (model == null)
                return;
           
            PhoneNumber = model.PhoneNumber;
            Fax = model.Fax;
            WebSite = model.WebSite;
        }
    }
}
