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

        public BaseDropdownableDetailsVM Region { get; set; }

        public BaseDropdownableDetailsVM City { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Ответственности")]
        public string Responsibility { get; set; }

        [Display(Name = "Требования")]
        public string Requirements { get; set; }

        [Display(Name = "Заработная плата")]
        public string Salary { get; set; }

        [Display(Name = "Контакты")]
        public string ContactPerson { get; set; }

        [Display(Name = "Контакты")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Контакты")]
        public string Email { get; set; }

        [Display(Name = "Статус")]
        public BaseDropdownableDetailsVM Status { get; set; }

        public VacancyDetailsVM(Vacancy model)
        {
            if (model == null)
                return;

            ID = model.ID;
            Company = model.Company;

            Region = new BaseDropdownableDetailsVM(model.Region);
            if (model.CityID.HasValue)
                City = new BaseDropdownableDetailsVM(model.City);
            else
            {
                City = new BaseDropdownableDetailsVM()
                {
                    ID = 0,
                    Name = model.CityOther ?? ""
                };
            }

            Position = model.Position;
            Responsibility = model.Responsibility;
            Requirements = model.Requirements;
            Salary = model.Salary;
            ContactPerson = model.ContactPerson;
            PhoneNumber = model.PhoneNumber;
            Email = model.Email;
            Status = new BaseDropdownableDetailsVM(model.VacancyStatus);
        }

        public List<VacancyDetailsVM> GetListOfVacancyDetailsVM(IQueryable<Vacancy> models)
        {
            if (models == null)
                return new List<VacancyDetailsVM>();

            return models.Select(s => new VacancyDetailsVM(s)).ToList();
        }
    }
}
