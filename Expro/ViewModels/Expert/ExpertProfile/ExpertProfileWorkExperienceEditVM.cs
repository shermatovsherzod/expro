using Expro.Models;
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

        [Required]
        [Display(Name = "Страна")]
        public int CountryID { get; set; }

        //[Required]
        [Display(Name = "Город")]
        [StringLength(256)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Место работы")]
        [StringLength(256)]
        public string PlaceOfWork { get; set; }

        [Required]
        [Display(Name = "Должность")]
        [StringLength(256)]
        public string Position { get; set; }

        [Required]
        [Display(Name = "С")]
        public string WorkPeriodFrom { get; set; }

        [Required]
        [Display(Name = "По")]
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
