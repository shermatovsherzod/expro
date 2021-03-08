using Expro.Common;
using Expro.Common.Utilities;
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
    public class ResumeEditVM
    {
        public ResumeEditVM() { }
        public int ID { get; set; }

       // [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [StringLength(100)]
        [Display(Name = "lblFirstName", ResourceType = typeof(ResourceTexts))]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "lblLastName", ResourceType = typeof(ResourceTexts))]
        public string LastName { get; set; }

        [StringLength(100)]
        [Display(Name = "lblPatronymicName", ResourceType = typeof(ResourceTexts))]
        public string PatronymicName { get; set; }

        [StringLength(400)]
        [Required]
        [Display(Name = "Contacts", ResourceType = typeof(ResourceTexts))]
        public string Contact { get; set; }

        [Required]
        [Display(Name = "lblDateOfBirth", ResourceType = typeof(ResourceTexts))]
        public string DateOfBirth { get; set; }

        [Display(Name = "lblRegion", ResourceType = typeof(ResourceTexts))]
        public int? RegionID { get; set; }

        [Display(Name = "lblCity", ResourceType = typeof(ResourceTexts))]
        public int? CityID { get; set; }

        [Display(Name = "lblCityOther", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string CityOther { get; set; }

        [StringLength(256)]
        [Display(Name = "Education", ResourceType = typeof(ResourceTexts))]
        public string Education { get; set; }

        [Display(Name = "lblGraduationYear", ResourceType = typeof(ResourceTexts))]
        public string GraduationDate { get; set; }

        [StringLength(256)]
        [Display(Name = "Workexperience", ResourceType = typeof(ResourceTexts))]
        public string WorkExperience { get; set; }

        [StringLength(256)]
        [Display(Name = "Languages", ResourceType = typeof(ResourceTexts))]
        public string Languages { get; set; }

        [StringLength(256)]
        [Display(Name = "OtherInfo", ResourceType = typeof(ResourceTexts))]
        public string OtherInfo { get; set; }

        public DocumentActionTypesEnum ActionType { get; set; } = DocumentActionTypesEnum.saveAsDraft;

        public int StatusID { get; set; }
        public ResumeEditVM(Resume model)
        {
            if (model == null)
                return;
            ID = model.ID;
            FirstName = model.FirstName;
            LastName = model.LastName;
            PatronymicName = model.PatronymicName;
            Contact = model.Contact;
            DateOfBirth = model.DateOfBirth != DateTime.MinValue ? DateTimeUtils.ConvertToString(model.DateOfBirth, AppData.Configuration.DateViewStringFormat) : "";
            RegionID = model.RegionID;
            CityID = model.CityID;
            CityOther = model.CityOther;
            Education = model.Education;
            GraduationDate = model.GraduationDate != DateTime.MinValue ? DateTimeUtils.ConvertToString(model.GraduationDate, AppData.Configuration.DateViewStringFormat) : "";
            WorkExperience = model.WorkExperience;
            Languages = model.Languages;
            OtherInfo = model.OtherInfo;
            StatusID = model.ResumeStatusID;
        }
    }
}
