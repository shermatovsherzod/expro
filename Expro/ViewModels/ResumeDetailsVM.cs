using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class ResumeDetailsVM
    {
        public ResumeDetailsVM() { }
        public int ID { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string PatronymicName { get; set; }

        [Display(Name = "Контакты")]
        public string Contact { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Регион")]
        public string Region { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Другой город")]
        public string CityOther { get; set; }

        [Display(Name = "Образования")]
        public string Education { get; set; }

        [Display(Name = "Дата окончания учебного заведения")]
        public DateTime? GraduationDate { get; set; }

        [Display(Name = "Стаж работы")]
        public string WorkExperience { get; set; }

        [Display(Name = "Знание языков")]
        public string Languages { get; set; }

        [Display(Name = "Дополнительная информация")]
        public string OtherInfo { get; set; }

        [Display(Name = "Статус")]
        public int Status { get; set; }

        public ResumeDetailsVM(Resume model)
        {
            if (model == null)
                return;
            ID = model.ID;
            FirstName = model.FirstName;
            LastName = model.LastName;
            PatronymicName = model.PatronymicName;
            Contact = model.Contact;
            DateOfBirth = model.DateOfBirth;
            Region = model.Region?.Name;
            City = model.City?.Name;
            CityOther = model.CityOther;
            Education = model.Education;
            GraduationDate = model.GraduationDate;
            WorkExperience = model.WorkExperience;
            Languages = model.Languages;
            OtherInfo = model.OtherInfo;
            Status = model.ResumeStatusID;
        }

        public List<ResumeDetailsVM> GetListOfResumeDetailsVM(IQueryable<Resume> models)
        {
            if (models == null)
                return new List<ResumeDetailsVM>();

            return models.Select(s => new ResumeDetailsVM
            {
                ID = s.ID,
                FirstName = s.FirstName,
                LastName = s.LastName,
                PatronymicName = s.PatronymicName,
                Contact = s.Contact,
                DateOfBirth = s.DateOfBirth,
                Region = s.Region != null ? s.Region.Name : "",
                City = s.City != null ? s.City.Name : "",
                CityOther = s.CityOther,
                Education = s.Education,
                GraduationDate = s.GraduationDate,
                WorkExperience = s.WorkExperience,
                Languages = s.Languages,
                OtherInfo = s.OtherInfo,

            }).ToList();
        }
    }
}
