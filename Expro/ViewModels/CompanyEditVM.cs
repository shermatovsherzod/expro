using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class CompanyEditVM
    {
        public CompanyEditVM() { }
        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Название компании")]
        public string CompanyName { get; set; }

        [StringLength(4000)]
        [Display(Name = "О компании")]
        public string CompanyDescription { get; set; }

        [Display(Name = "Регион")]
        public int? RegionID { get; set; }

        [Display(Name = "Город")]
        public int? CityID { get; set; }

        [Display(Name = "Другой город")]
        [StringLength(256)]
        public string CityOther { get; set; }

        [Required(ErrorMessage = "Поле Направление обязательно для заполнения")]
        [Display(Name = "Направление")]
        public List<int> LawAreas { get; set; }

        [StringLength(100)]
        [Display(Name = "Сайт")]
        public string WebSite { get; set; }

        [StringLength(100)]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Емейл")]
        public string Email { get; set; }

        [StringLength(256)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        public DocumentActionTypesEnum ActionType { get; set; } = DocumentActionTypesEnum.saveAsDraft;

        public int StatusID { get; set; }

        public AttachmentDetailsVM Logo { get; set; }

        public CompanyEditVM(Company model)
        {
            if (model == null)
                return;

            ID = model.ID;
            CompanyName = model.CompanyName;
            CompanyDescription = model.CompanyDescription;
            RegionID = model.RegionID;
            CityID = model.CityID;
            CityOther = model.CityOther;
            LawAreas = model.CompanyLawAreas != null ? model.CompanyLawAreas.Select(r => (int)r.LawAreaID).ToList() : null;
            WebSite = model.WebSite;
            PhoneNumber = model.PhoneNumber;
            Email = model.Email;
            Address = model.Address;
            Logo = new AttachmentDetailsVM(model.Logo);
            StatusID = model.CompanyStatusID;
        }
    }
}
