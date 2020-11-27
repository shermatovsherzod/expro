using Expro.Common.Utilities;
using Expro.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Expro.ViewModels
{
    public class ExpertProfileMainInfoEditVM : AppUserVM
    {
        public ExpertProfileMainInfoEditVM() { }

        [Display(Name = "Отчество")]
        [StringLength(256)]
        public string PatronymicName { get; set; }

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

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Поле Дата рождения обязательно для заполнения")]
        public string DateOfBirth { get; set; }        

        [Display(Name = "Пол")]
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
            LawAreas = user.UserLawAreas != null ? user.UserLawAreas.Select(r => (int)r.LawAreaID).ToList() : null;
            DateOfBirth = DateTimeUtils.ConvertToString(user.DateOfBirth);
            GenderID = user.Gender?.ID;
        }
    }
}
