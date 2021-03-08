using Expro.Common.Utilities;
using Expro.Models;
using Expro.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Expro.ViewModels
{
    public class ExpertProfileMainInfoEditVM : AppUserVM
    {
        public ExpertProfileMainInfoEditVM() { }

        [Display(Name = "lblPatronymicName", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string PatronymicName { get; set; }

        [Display(Name = "lblRegion", ResourceType = typeof(ResourceTexts))]
        public int? RegionID { get; set; }

        [Display(Name = "lblCity", ResourceType = typeof(ResourceTexts))]
        public int? CityID { get; set; }

        [Display(Name = "lblCityOther", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string CityOther { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblLawAreas", ResourceType = typeof(ResourceTexts))]
        public int LawAreaParentID { get; set; }

        public List<int> LawAreas { get; set; }

        [Display(Name = "lblDateOfBirth", ResourceType = typeof(ResourceTexts))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        public string DateOfBirth { get; set; }        

        [Display(Name = "lblGender", ResourceType = typeof(ResourceTexts))]
        public int? GenderID { get; set; }

        public ExpertProfileMainInfoEditVM(ApplicationUser user)
           : base(user)
        {           
            if (user == null)
                return;
            ID = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PatronymicName = user.PatronymicName;
            RegionID = user.Region?.ID;
            CityID = user.City?.ID;
            CityOther = user.CityOther;
            LawAreaParentID = user.LawAreaParentID ?? 0;
            LawAreas = user.UserLawAreas != null ? user.UserLawAreas.Select(r => (int)r.LawAreaID).ToList() : null;
            DateOfBirth = DateTimeUtils.ConvertToString(user.DateOfBirth);
            GenderID = user.Gender?.ID;
        }
    }
}
