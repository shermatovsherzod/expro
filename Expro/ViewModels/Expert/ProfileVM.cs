using Expro.Common.Utilities;
using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expro.Models
{
    public class ExpertProfileEditVM
    {
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
        public int RegionID { get; set; }

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

        [Required]
        [Display(Name = "Пол")]
        public int? GenderID { get; set; }

        public ExpertProfileEditVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;

            FirstName = model.FirstName;
            LastName = model.LastName;
            
            DateOfBirth = DateTimeUtils.ConvertToString(model.DateOfBirth, "dd/MM/yyyy");
            GenderID = model.GenderID;
        }
    }
}
