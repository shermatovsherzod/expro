using Expro.Models;
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

        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }


        public ExpertProfileContactVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;

           // Email = model.Email;
            PhoneNumber = model.PhoneNumber;
        }
    }
}
