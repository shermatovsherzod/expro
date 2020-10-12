using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Models
{
    public class ExpertProfileEducationFormVM
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Страна")]
        public int CountryID { get; set; }

        //[Required]
        [Display(Name = "Город")]
        public int? CityID { get; set; }

        [Display(Name = "Город")]
        public string CityOther { get; set; }

        [Required]
        [Display(Name = "ВУЗ")]
        public string University { get; set; }

        [Required]
        [Display(Name = "Факультет")]
        public string Faculty { get; set; }

        [Required]
        [Display(Name = "Год окончания")]
        public int GraduationYear { get; set; }

        public ExpertProfileEducationFormVM(Education model) // : base(model)
        {
            if (model == null)
                return;

            //Email = model.Email;
            //PhoneNumber = model.PhoneNumber;
        }
    }

    public class EducationListItemVM
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        //[Required]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required]
        [Display(Name = "ВУЗ")]
        public string University { get; set; }

        [Required]
        [Display(Name = "Факультет")]
        public string Faculty { get; set; }

        [Required]
        [Display(Name = "Год окончания")]
        public int GraduationYear { get; set; }

        public EducationListItemVM(Education model) // : base(model)
        {
            if (model == null)
                return;

            //Email = model.Email;
            //PhoneNumber = model.PhoneNumber;
        }
    }
}
