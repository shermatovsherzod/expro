using Expro.Common.Utilities;
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

        public string FullName
        {
            get
            {
                return LastName + " " + FirstName + " " + PatronymicName;
            }
        }

        [Display(Name = "Контакты")]
        public string Contact { get; set; }

        [Display(Name = "Дата рождения")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Регион")]
        public BaseDropdownableDetailsVM Region { get; set; }

        public BaseDropdownableDetailsVM City { get; set; }

        [Display(Name = "Образование")]
        public string Education { get; set; }

        [Display(Name = "Дата окончания учебного заведения")]
        public string GraduationDate { get; set; }

        [Display(Name = "Стаж работы")]
        public string WorkExperience { get; set; }

        [Display(Name = "Знание языков")]
        public string Languages { get; set; }

        [Display(Name = "Дополнительная информация")]
        public string OtherInfo { get; set; }

        [Display(Name = "Статус")]
        public BaseDropdownableDetailsVM Status { get; set; }

        public ResumeDetailsVM(Resume model)
        {
            if (model == null)
                return;
            ID = model.ID;
            FirstName = model.FirstName;
            LastName = model.LastName;
            PatronymicName = model.PatronymicName;
            Contact = model.Contact;
            DateOfBirth = DateTimeUtils.ConvertToString(model.DateOfBirth);

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

            Education = model.Education;
            GraduationDate = DateTimeUtils.ConvertToString(model.GraduationDate);
            WorkExperience = model.WorkExperience;
            Languages = model.Languages;
            OtherInfo = model.OtherInfo;
            Status = new BaseDropdownableDetailsVM(model.ResumeStatus);
        }

        public List<ResumeDetailsVM> GetListOfResumeDetailsVM(IQueryable<Resume> models)
        {
            if (models == null)
                return new List<ResumeDetailsVM>();

            return models.Select(s => new ResumeDetailsVM(s)).ToList();
        }
    }
}
