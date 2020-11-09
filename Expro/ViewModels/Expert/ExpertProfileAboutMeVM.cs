using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels.Expert
{
    public class ExpertProfileAboutMeVM
    {
        public ExpertProfileAboutMeVM() { }
               
        [Display(Name = "Обо мне")]
        [MaxLength(2000)]
        public string AboutMe { get; set; }


        public ExpertProfileAboutMeVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;
            AboutMe = model.AboutMe;          
        }
    }
}
