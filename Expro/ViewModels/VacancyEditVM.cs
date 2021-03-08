using Expro.Models;
using Expro.Models.Enums;
using Expro.Resources;
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

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
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
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        public string Responsibility { get; set; }

        [StringLength(2000)]
        [Display(Name = "Требования")]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        public string Requirements { get; set; }

        [StringLength(256)]
        [Display(Name = "Заработная плата")]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        public string Salary { get; set; }

        [StringLength(256)]
        [Display(Name = "Контактное лицо")]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        public string ContactPerson { get; set; }

        [StringLength(256)]
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Емейл")]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
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
