using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Resources;
using Expro.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Expro.ViewModels.SimpleUser
{
    public class ProfileSimpleUserVM
    {
        public ProfileSimpleUserVM()
        {

        }

        [Required]
        [Display(Name = "lblFirstName", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "lblLastName", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string LastName { get; set; }

        [Display(Name = "lblPatronymicName", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string PatronymicName { get; set; }

        [Display(Name = "Регион")]
        public int? RegionID { get; set; }

        [Display(Name = "lblRegion", ResourceType = typeof(ResourceTexts))]
        public string Region { get; set; }

        [Display(Name = "lblCity", ResourceType = typeof(ResourceTexts))]
        public int? CityID { get; set; }

        [Display(Name = "lblCityOther", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        //[Remote("ValidateFrom", "VideoRequest", ErrorMessage = "Введите город", AdditionalFields = "TypeID")]
        public string CityOther { get; set; }

        [Display(Name = "lblDateOfBirth", ResourceType = typeof(ResourceTexts))]
        [Required(ErrorMessage = "Поле Дата рождения обязательно для заполнения")]
        public string DateOfBirth { get; set; }

        [Display(Name = "lblGender", ResourceType = typeof(ResourceTexts))]
        public int? GenderID { get; set; }

        [Display(Name = "lblDateRegistered", ResourceType = typeof(ResourceTexts))]
        public string DateRegistered { get; set; }

        public ProfileSimpleUserVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;

            FirstName = model.FirstName;
            LastName = model.LastName;
            PatronymicName = model.PatronymicName;
            RegionID = model.RegionID;
            CityID = model.CityID;
            CityOther = model.CityOther;
            DateOfBirth = DateTimeUtils.ConvertToString(model.DateOfBirth);
            GenderID = model.GenderID;
            Region = model.Region != null ? model.Region.Name : "";
            DateRegistered = DateTimeUtils.ConvertToString(model.DateRegistered);
        }
    }
}
