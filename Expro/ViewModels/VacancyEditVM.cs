using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class VacancyEditVM
    {
        public VacancyEditVM() { }
        public int ID { get; set; }

        [StringLength(400)]
        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Название компании")]
        public string Company { get; set; }

        [Display(Name = "Регион")]
        public int? RegionID { get; set; }

        [Display(Name = "Город")]
        public int? CityID { get; set; }

        [Display(Name = "Другой город")]
        [StringLength(256)]
        public string CityOther { get; set; }

        [StringLength(2000)]
        [Display(Name = "Ответственности")]
        [Required]
        public string Responsibility { get; set; }

        [StringLength(2000)]
        [Display(Name = "Требования")]
        [Required]
        public string Requirements { get; set; }

        [StringLength(256)]
        [Display(Name = "Заработная плата")]
        [Required]
        public string Salary { get; set; }

        [StringLength(256)]
        [Display(Name = "Контактное лицо")]
        [Required]
        public string ContactPerson { get; set; }

        [StringLength(256)]
        [Display(Name = "Номер телефона")]
        [Required]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Емейл")]
        [Required]
        public string Email { get; set; }

        public DocumentActionTypesEnum ActionType { get; set; } = DocumentActionTypesEnum.saveAsDraft;

        public int StatusID { get; set; }

        public VacancyEditVM(Vacancy model)
        {
            if (model == null)
                return;

            ID = model.ID;
            Company = model.Company;
            RegionID = model.RegionID;
            CityID = model.CityID;
            CityOther = model.CityOther;
            Position = model.Position;
            Responsibility = model.Responsibility;
            Requirements = model.Requirements;
            Salary = model.Salary;
            ContactPerson = model.ContactPerson;
            PhoneNumber = model.PhoneNumber;
            Email = model.Email;
        }
    }
}
