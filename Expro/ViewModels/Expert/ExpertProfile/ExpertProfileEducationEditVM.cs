using Expro.Models;
using Expro.Resources;
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

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblCountry", ResourceType = typeof(ResourceTexts))]
        public int CountryID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblCity", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblUniversity", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string University { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblFaculty", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string Faculty { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblGraduationYear", ResourceType = typeof(ResourceTexts))]
        public int GraduationYear { get; set; }

        public ExpertProfileEducationEditVM(ExpertEducation model)
        {
            if (model == null)
                return;

            ID = model.ID;
            CountryID = model.CountryID;
            City = model.City;
            University = model.University;
            Faculty = model.Faculty;
            GraduationYear = model.GraduationYear;
        }
    }

}
