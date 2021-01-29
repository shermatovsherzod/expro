using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class ExpertProfileEducationDetailsVM
    {
        public ExpertProfileEducationDetailsVM() { }

        public int ID { get; set; }

        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "ВУЗ")]
        public string University { get; set; }

        [Display(Name = "Факультет")]
        public string Faculty { get; set; }

        [Display(Name = "Год окончания")]
        public int GraduationYear { get; set; }

        public ExpertProfileEducationDetailsVM(ExpertEducation model)
        {
            if (model == null)
                return;

            ID = model.ID;
            Country = model.Country?.Name;
            City = model.City;
            University = model.University;
            Faculty = model.Faculty;
            GraduationYear = model.GraduationYear;
        }

        public List<ExpertProfileEducationDetailsVM> GetListOfExpertProfileEducationDetailsVM(IQueryable<ExpertEducation> models)
        {
            if (models == null)
                return new List<ExpertProfileEducationDetailsVM>();
           
           return models.Select(s => new ExpertProfileEducationDetailsVM
             {
                 University = s.University,
                 City = s.City,
                 Country = s.Country.Name,
                 Faculty = s.Faculty,
                 GraduationYear = s.GraduationYear,
                 ID = s.ID
             }).ToList();
        }

        public List<ExpertProfileEducationDetailsVM> GetListOfExpertProfileEducationDetailsVM(ICollection<ExpertEducation> models)
        {
            if (models == null)
                return null;

            return models.Select(s => new ExpertProfileEducationDetailsVM
            {
                University = s.University,
                City = s.City,
                Country = s.Country.Name,
                Faculty = s.Faculty,
                GraduationYear = s.GraduationYear,
                ID = s.ID
            }).ToList();
        }
    }
}
