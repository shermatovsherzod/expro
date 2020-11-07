using Expro.Common.Utilities;
using Expro.Models.Enums;
using Expro.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Expro.Models
{
    public class ProfileSimpleUserVM
    {
        public ProfileSimpleUserVM()
        {

        }

        [Required]
        [Display(Name = "Имя")]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(256)]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [StringLength(256)]
        public string PatronymicName { get; set; }

        [Display(Name = "Регион")]
        public int? RegionID { get; set; }

        [Display(Name = "Регион")]
        public string Region { get; set; }

        [Display(Name = "Город")]
        public int? CityID { get; set; }

        [Display(Name = "Другой город")]
        [StringLength(256)]
        //[Remote("ValidateFrom", "VideoRequest", ErrorMessage = "Введите город", AdditionalFields = "TypeID")]
        public string CityOther { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Поле Дата рождения обязательно для заполнения")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Пол")]
        public int? GenderID { get; set; }

        [Display(Name = "На сайте с:")]
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
