using Expro.Common.Utilities;
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
        public int ID { get; set; }

        public AttachmentDetailsVM Logo { get; set; }

        [Display(Name = "Название компании")]
        public string CompanyName { get; set; }

        [Display(Name = "Сайт")]
        public string WebSite { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Емейл")]
        public string Email { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "О компании")]
        public string CompanyDescription { get; set; }

        public BaseDropdownableDetailsVM Region { get; set; }

        public BaseDropdownableDetailsVM City { get; set; }

        public BaseDropdownableDetailsVM Status { get; set; }

        [Display(Name = "Направление")]
        public List<BaseDropdownableDetailsVM> LawAreas { get; set; }

        [Display(Name = "Дата изменения")]
        public string DateModified { get; set; }

        public CompanyDetailsVM() { }

        public CompanyDetailsVM(Company model)
        {
            if (model == null)
                return;

            ID = model.ID;
            CompanyName = model.CompanyName;
            Logo = new AttachmentDetailsVM(model.Logo);
            WebSite = model.WebSite;
            PhoneNumber = model.PhoneNumber;
            Email = model.Email;
            Address = model.Address;
            CompanyDescription = model.CompanyDescription;

            LawAreas = new List<BaseDropdownableDetailsVM>();
            if (model.LawAreaParent != null)
                LawAreas.Add(new BaseDropdownableDetailsVM(model.LawAreaParent));
            LawAreas.AddRange(model.CompanyLawAreas
                .Select(m => new BaseDropdownableDetailsVM(m.LawArea))
                .ToList());
            

            Status = new BaseDropdownableDetailsVM(model.CompanyStatus);
            DateModified = DateTimeUtils.ConvertToString(model.DateModified);

            Region = new BaseDropdownableDetailsVM(model.Region);
            if (model.CityID.HasValue)
                City = new BaseDropdownableDetailsVM(model.City);
            else
            {
                City = new BaseDropdownableDetailsVM()
                {
                    ID = 0,
                    Name = model.CityOther
                };
            }
        }

        public List<CompanyDetailsVM> GetListOfCompanyDetailsVM(IQueryable<Company> models)
        {
            if (models == null)
                return new List<CompanyDetailsVM>();

            return models.Select(s => new CompanyDetailsVM(s)).ToList();
        }
    }
}
