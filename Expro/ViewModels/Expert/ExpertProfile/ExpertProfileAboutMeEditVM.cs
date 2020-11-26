using Expro.Models;
using System.ComponentModel.DataAnnotations;

namespace Expro.ViewModels
{
    public class ExpertProfileAboutMeEditVM 
    {
        public ExpertProfileAboutMeEditVM() { }
               
        [Display(Name = "Обо мне")]
        [MaxLength(2000)]
        public string AboutMe { get; set; }

        public ExpertProfileAboutMeEditVM(ApplicationUser user)            
        {
            if (user == null)
                return;
            AboutMe = user.AboutMe;          
        }
    }
}
