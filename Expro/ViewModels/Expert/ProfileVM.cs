using Expro.Common.Utilities;
using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Expro.Models
{
    public class ExpertProfileMainInfoVM
    {
        public ExpertProfileMainInfoVM()
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

        [Display(Name = "Город")]
        public int? CityID { get; set; }

        [Display(Name = "Другой город")]
        [StringLength(256)]
        //[Remote("ValidateFrom", "VideoRequest", ErrorMessage = "Введите город", AdditionalFields = "TypeID")]
        public string CityOther { get; set; }

        [Required]
        [Display(Name = "Направление")]
        public List<int> LawAreas { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        public string DateOfBirth { get; set; }

 
        [Display(Name = "Пол")]
        public int? GenderID { get; set; }

        public ExpertProfileMainInfoVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;

            FirstName = model.FirstName;
            LastName = model.LastName;
            PatronymicName = model.PatronymicName;
            RegionID = model.RegionID;
            CityID = model.CityID;
            CityOther = model.CityOther;
            LawAreas = model.UserLawAreas != null ? model.UserLawAreas.Select(r => (int)r.LawAreaID).ToList() : null;
            DateOfBirth = DateTimeUtils.ConvertToString(model.DateOfBirth, "dd/MM/yyyy");
            GenderID = model.GenderID;
        }
    }

    public class ExpertProfileContactVM
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }


        public ExpertProfileContactVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;

            Email = model.Email;
            PhoneNumber = model.PhoneNumber;
        }
    }
}
