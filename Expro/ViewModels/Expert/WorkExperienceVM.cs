using Expro.Models;
using Expro.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Models
{
    public class ExpertProfileWorkExperienceFormVM
    {
        public ExpertProfileWorkExperienceFormVM() { }
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "Страна")]
        public int CountryID { get; set; }

        //[Required]
        [Display(Name = "Город")]
        [StringLength(256)]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "Место работы")]
        [StringLength(256)]
        public string PlaceOfWork { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "Должность")]
        [StringLength(256)]
        public string Position { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "С")]
        public string WorkPeriodFrom { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "По")]
        public string WorkPeriodTo { get; set; }

       

        public ExpertProfileWorkExperienceFormVM(WorkExperience model) // : base(model)
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

    public class WorkExperienceListItemVM
    {
        public int ID { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Место работы")]
        public string PlaceOfWork { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "С")]
        public string WorkPeriodFrom { get; set; }

        [Display(Name = "По")]
        public string WorkPeriodTo { get; set; }

        public List<WorkExperienceListItemVM> GetWorkExperienceListVM(IQueryable<WorkExperience> models, string UserID)
        {
            if (models == null)
                return new List<WorkExperienceListItemVM>();

            return models.Where(c => c.Creator.Id == UserID).Select(model => new WorkExperienceListItemVM
            {
                ID = model.ID,
                Country = model.Country.Name,
                City = model.City,
                PlaceOfWork = model.PlaceOfWork,
                Position = model.Position,
                WorkPeriodFrom = model.WorkPeriodFrom,
                WorkPeriodTo = model.WorkPeriodTo,
            }).ToList();
        }
    }
}
