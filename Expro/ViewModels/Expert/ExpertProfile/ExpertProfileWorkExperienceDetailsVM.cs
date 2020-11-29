using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class ExpertProfileWorkExperienceDetailsVM
    {
        public ExpertProfileWorkExperienceDetailsVM() { }
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

        public ExpertProfileWorkExperienceDetailsVM(WorkExperience model) 
        {
            if (model == null)
                return;

            ID = model.ID;
            Country = model.Country?.Name;
            City = model.City;
            PlaceOfWork = model.PlaceOfWork;
            Position = model.Position;
            WorkPeriodFrom = model.WorkPeriodFrom;
            WorkPeriodTo = model.WorkPeriodTo;
        }

        public List<ExpertProfileWorkExperienceDetailsVM> GetListOfExpertProfileWorkExperienceDetailsVM(IQueryable<WorkExperience> models)
        {
            if (models == null)
                return new List<ExpertProfileWorkExperienceDetailsVM>();

            return models.Select(s => new ExpertProfileWorkExperienceDetailsVM
            {
                ID = s.ID,
                Country = s.Country.Name,
                City = s.City,
                PlaceOfWork = s.PlaceOfWork,
                Position = s.Position,
                WorkPeriodFrom = s.WorkPeriodFrom,
                WorkPeriodTo = s.WorkPeriodTo
            }).ToList();
        }
    }
}
