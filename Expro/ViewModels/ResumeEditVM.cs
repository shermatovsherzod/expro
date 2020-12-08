using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class ResumeEditVM
    {
        public ResumeEditVM() { }
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "Отчество")]
        public string PatronymicName { get; set; }

        [StringLength(400)]
        [Required]
        [Display(Name = "Контакты")]
        public string Contact { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Регион")]
        public int? RegionID { get; set; }

        [Display(Name = "Город")]
        public int? CityID { get; set; }

        [Display(Name = "Другой город")]
        [StringLength(256)]
        public string CityOther { get; set; }

        [StringLength(256)]
        [Display(Name = "Образования")]
        public string Education { get; set; }

        [Display(Name = "Дата окончания учебного заведения")]
        public DateTime? GraduationDate { get; set; }

        [StringLength(256)]
        [Display(Name = "Стаж работы")]
        public string WorkExperience { get; set; }

        [StringLength(256)]
        [Display(Name = "Знание языков")]
        public string Languages { get; set; }

        [StringLength(256)]
        [Display(Name = "Дополнительная информация")]
        public string OtherInfo { get; set; }

        public ResumeEditVM(Resume model)
        {
            if (model == null)
                return;
            ID = model.ID;
            FirstName = model.FirstName;
            LastName = model.LastName;
            PatronymicName = model.PatronymicName;
            Contact = model.Contact;
            DateOfBirth = model.DateOfBirth;
            RegionID = model.RegionID;
            CityID = model.CityID;
            CityOther = model.CityOther;
            Education = model.Education;
            GraduationDate = model.GraduationDate;
            WorkExperience = model.WorkExperience;
            Languages = model.Languages;
            OtherInfo = model.OtherInfo;
        }
    }
}
