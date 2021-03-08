using Expro.Models;
using Expro.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class ExpertProfileWorkExperienceEditVM
    {
        public ExpertProfileWorkExperienceEditVM() { }
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblCountry", ResourceType = typeof(ResourceTexts))]
        public int CountryID { get; set; }

        //[Required]
        [Display(Name = "lblCity", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblPlaceOfWork", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string PlaceOfWork { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblPosition", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string Position { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblWorkPeriodFrom", ResourceType = typeof(ResourceTexts))]
        public string WorkPeriodFrom { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblWorkPeriodTo", ResourceType = typeof(ResourceTexts))]
        public string WorkPeriodTo { get; set; }

        public ExpertProfileWorkExperienceEditVM(WorkExperience model) 
        {
            if (model == null)
                return;

            ID = model.ID;
            CountryID = model.ID;
            City = model.City;
            PlaceOfWork = model.PlaceOfWork;
            Position = model.Position;
            WorkPeriodFrom = model.WorkPeriodFrom;
            WorkPeriodTo = model.WorkPeriodTo;
        }
    }
}
