using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expro.Models
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Почтовый адрес")]
        public string Email { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string PatronymicName { get; set; }
        public UserTypesEnum UserType { get; set; }

        [Display(Name = "Город")]
        public int CityID { get; set; }

        [Display(Name = "Город")]
        public string City { get; set; }

        [Display(Name = "Другой город")]
        public string CityOther { get; set; }

        [Display(Name = "Регион")]
        public int RegionID { get; set; }

        [Display(Name = "Регион")]
        public string Region { get; set; }

        [Display(Name = "Пол")]
        public int? GenderID { get; set; }

        [Display(Name = "Пол")]
        public string Gender { get; set; }

        public List<EducationVM> educationVMs { get; set; }

    }
}
