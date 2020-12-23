using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class VacancyDetailsVM
    {
        public VacancyDetailsVM() { }
        public int ID { get; set; }

        [Display(Name = "Название компании")]
        public string Company { get; set; }

        [Display(Name = "Регион")]
        public string Region { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Другой город")]
        public string CityOther { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Ответственности")]
        public string Responsibility { get; set; }

        [Display(Name = "Требования")]
        public string Requirements { get; set; }

        [Display(Name = "Заработная плата")]
        public string Salary { get; set; }

        [Display(Name = "Контакты")]
        public string Contacts { get; set; }

        [Display(Name = "Статус")]
        public int Status { get; set; }

        public VacancyDetailsVM(Vacancy model)
        {
            if (model == null)
                return;

            ID = model.ID;
            Company = model.Company;
            Region = model.Region?.Name;
            City = model.City?.Name;
            CityOther = model.CityOther;
            Position = model.Position;
            Responsibility = model.Responsibility;
            Requirements = model.Requirements;
            Salary = model.Salary;
            Contacts = model.Contacts;
            Status = model.VacancyStatusID;
        }

        public List<VacancyDetailsVM> GetListOfVacancyDetailsVM(IQueryable<Vacancy> models)
        {
            if (models == null)
                return new List<VacancyDetailsVM>();

            return models.Select(s => new VacancyDetailsVM
            {
                ID = s.ID,
                Company = s.Company,
                Region = s.Region != null ? s.Region.Name : "",
                City = s.City != null ? s.City.Name : "",
                CityOther = s.CityOther,
                Position = s.Position,
                Responsibility = s.Responsibility,
                Requirements = s.Requirements,
                Salary = s.Salary,
                Contacts = s.Contacts,
            }).ToList();
        }
    }
}
