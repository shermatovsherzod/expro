using Expro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class CompanyDetailsVM
    {
        public CompanyDetailsVM() { }
        public int ID { get; set; }

        [Display(Name = "Название компании")]
        public string CompanyName { get; set; }

        [Display(Name = "О компании")]
        public string CompanyDescription { get; set; }

        [Display(Name = "Регион")]
        public string Region { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Другой город")]
        public string CityOther { get; set; }

        [Display(Name = "Направление")]
        public List<string> LawAreas { get; set; }

        [Display(Name = "Сайт")]
        public string WebSite { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Емейл")]
        public string Email { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Статус")]
        public int Status { get; set; }

        public CompanyDetailsVM(Company model)
        {
            if (model == null)
                return;

            ID = model.ID;
            CompanyName = model.CompanyName;
            CompanyDescription = model.CompanyDescription;
            Region = model.Region?.Name;
            City = model.City?.Name;
            CityOther = model.CityOther;
            LawAreas = model.CompanyLawAreas != null ? model.CompanyLawAreas.Select(r => r.LawArea.Name).ToList() : null;
            WebSite = model.WebSite;
            PhoneNumber = model.PhoneNumber;
            Email = model.Email;
            Address = model.Address;
            Status = model.CompanyStatusID;
        }

        public List<CompanyDetailsVM> GetListOfCompanyDetailsVM(IQueryable<Company> models)
        {
            if (models == null)
                return new List<CompanyDetailsVM>();

            return models.Select(s => new CompanyDetailsVM
            {
                ID = s.ID,
                CompanyName = s.CompanyName,
                CompanyDescription = s.CompanyDescription,
                Region = s.Region.Name,
                City = s.City != null ? s.City.Name : "",
                CityOther = s.CityOther,
                LawAreas = s.CompanyLawAreas != null ? s.CompanyLawAreas.Select(r => r.LawArea.Name).ToList() : null,
                WebSite = s.WebSite,
                PhoneNumber = s.PhoneNumber,
                Email = s.Email,
                Address = s.Address
            }).ToList();
        }
    }
}
