using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class ExpertProfileEducationEditVM
    {
        public ExpertProfileEducationEditVM() { }

        public int ID { get; set; }

        [Required]
        [Display(Name = "Страна")]
        public int CountryID { get; set; }

        [Required]
        [Display(Name = "Город")]
        [StringLength(256)]
        public string City { get; set; }      

        [Required]
        [Display(Name = "ВУЗ")]
        [StringLength(256)]
        public string University { get; set; }

        [Required]
        [Display(Name = "Факультет")]
        [StringLength(256)]
        public string Faculty { get; set; }

        [Required]
        [Display(Name = "Год окончания")]
        public int GraduationYear { get; set; }

        public ExpertProfileEducationEditVM(Education model)
        {
            if (model == null)
                return;
            
            ID = model.ID;
            CountryID = model.ID;
            City = model.City;            
            University = model.University;
            Faculty = model.Faculty;
            GraduationYear = model.GraduationYear;           
        }
    }

}
