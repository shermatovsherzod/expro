using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Models
{
    public class ExpertProfileEducationFormVM
    {
        public ExpertProfileEducationFormVM() { }

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

        public string UserID { get; set; }

        public ExpertProfileEducationFormVM(Education model) // : base(model)
        {
            if (model == null)
                return;

            ID = model.ID;
            CountryID = model.ID;
            City = model.City;            
            University = model.University;
            Faculty = model.Faculty;
            GraduationYear = model.GraduationYear;
            UserID = model.UserID;
        }
    }

    public class EducationListItemVM
    {
        public int ID { get; set; }
             
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Страна")]
        public int CountryID { get; set; }

        [Required]
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

        public string UserID { get; set; }

        //public EducationListItemVM(Education model) // : base(model)
        //{
        //    if (model == null)
        //        return;

        //    ID = model.ID;
        //    Country = model.Country.Name;
        //    City = model.CityID != null ? model.City.Name : "";
        //    CityOther = model.CityOther != null ? model.CityOther : "";
        //    University = model.University;
        //    Faculty = model.Faculty;
        //    GraduationYear = model.GraduationYear;
        //}
    }
}
