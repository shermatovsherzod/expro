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
        [Display(Name = "CompanyName", ResourceType = typeof(Resources.ResourceTexts))]
        public string CompanyName { get; set; }

        [StringLength(4000)]
        [Display(Name = "AboutCompany", ResourceType = typeof(Resources.ResourceTexts))]
        public string CompanyDescription { get; set; }

        [Display(Name = "lblRegion", ResourceType = typeof(Resources.ResourceTexts))]
        public int? RegionID { get; set; }

        [Display(Name = "lblCity", ResourceType = typeof(Resources.ResourceTexts))]
        public int? CityID { get; set; }

        [Display(Name = "lblCityOther", ResourceType = typeof(Resources.ResourceTexts))]
        [StringLength(256)]
        public string CityOther { get; set; }

        [Display(Name = "lblLawAreas", ResourceType = typeof(Resources.ResourceTexts))]
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceTexts), ErrorMessageResourceName = "ErrorRequiredField")]
        public int LawAreaParentID { get; set; }
        public List<int> LawAreas { get; set; }

        [StringLength(100)]
        [Display(Name = "lblWebSite", ResourceType = typeof(Resources.ResourceTexts))]
        public string WebSite { get; set; }

        [StringLength(100)]
        [Display(Name = "lblPhoneNumber", ResourceType = typeof(Resources.ResourceTexts))]
        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Email", ResourceType = typeof(Resources.ResourceTexts))]
        public string Email { get; set; }

        [StringLength(256)]
        [Display(Name = "Address", ResourceType = typeof(Resources.ResourceTexts))]
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
            LawAreaParentID = model.LawAreaParentID ?? 0;
            LawAreas = model.CompanyLawAreas != null ? model.CompanyLawAreas.Select(r => r.LawAreaID).ToList() : null;
            WebSite = model.WebSite;
            PhoneNumber = model.PhoneNumber;
            Email = model.Email;
            Address = model.Address;
            Logo = new AttachmentDetailsVM(model.Logo);
            StatusID = model.CompanyStatusID;
        }
    }
}
