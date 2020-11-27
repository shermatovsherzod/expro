using Expro.Models;
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

        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Факс")]
        public string Fax { get; set; }

        [Required]
        [Display(Name = "Веб сайт")]
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
