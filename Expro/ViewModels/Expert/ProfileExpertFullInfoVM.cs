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
    public class ProfileExpertFullInfoVM
    {
        public ProfileExpertFullInfoVM()
        {

        }
        public string Id { get; set; }

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
        public string RegionStr { get; set; }

        [Display(Name = "Город")]
        public int? CityID { get; set; }

        [Display(Name = "Город")]
        public string CityStr { get; set; }

        [Display(Name = "Другой город")]
        [StringLength(256)]
        //[Remote("ValidateFrom", "VideoRequest", ErrorMessage = "Введите город", AdditionalFields = "TypeID")]
        public string CityOther { get; set; }

        [Required(ErrorMessage = "Поле Направление обязательно для заполнения")]
        [Display(Name = "Направление")]
        public List<int> LawAreas { get; set; }

        [Display(Name = "Дата рождения")]
        [Required(ErrorMessage = "Поле Дата рождения обязательно для заполнения")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Пол")]
        public int? GenderID { get; set; }

        [Display(Name = "Пол")]
        public string GenderStr { get; set; }

        [Display(Name = "На сайте с:")]
        public string DateRegistered { get; set; }

        [Display(Name = "Обо мне")]
        public string AboutMe { get; set; }

        [Display(Name = "Статус")]
        public int ApproveStatus { get; set; }

        [Display(Name = "Статус")]
        public string ApproveStatusStr { get; set; }


        [Display(Name = "Дата подтверждения статуса")]
        public DateTime? DateApproved { get; set; }

        [Display(Name = "Дата отклонения статуса")]
        public DateTime? DateRejected { get; set; }

        [Display(Name = "Дата отправления статуса на подтверждение")]
        public DateTime? DateSubmittedForApproval { get; set; }


        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Емейл")]
        public string Email { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Счет")]
        public string AccountNumber { get; set; }

        [Display(Name = "Баланс")]
        public int Balance { get; set; }

        [Display(Name = "Факс")]
        public string Fax { get; set; }

        [Display(Name = "Сайт")]
        public string WebSite { get; set; }

        [Display(Name = "Направление")]
        public string LawAreaStr { get; set; }


        public ProfileExpertFullInfoVM(ApplicationUser model) // : base(model)
        {
            if (model == null)
                return;
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            PatronymicName = model.PatronymicName;
            RegionID = model.RegionID;
            CityID = model.CityID;
            CityOther = model.CityOther;
            LawAreas = model.UserLawAreas != null ? model.UserLawAreas.Select(r => (int)r.LawAreaID).ToList() : null;
            DateOfBirth = DateTimeUtils.ConvertToString(model.DateOfBirth);
            GenderID = model.GenderID;
            GenderStr = model.GenderID != null ? model.Gender.Name : "";
            DateRegistered = DateTimeUtils.ConvertToString(model.DateRegistered);
            ApproveStatus = model.ApproveStatus;
            DateApproved = model.DateApproved;
            DateRejected = model.DateRejected;
            DateSubmittedForApproval = model.DateSubmittedForApproval;
            AboutMe = model.AboutMe;
            UserName = model.UserName;
            Email = model.Email;
            PhoneNumber = model.PhoneNumber;
            AccountNumber = model.AccountNumber;
            Balance = model.Balance;
            Fax = model.Fax;
            WebSite = model.WebSite;
            RegionStr = model.RegionID != null ? model.Region.Name : "";
            CityStr = model.CityID != null ? model.City.Name : "";

            switch (model.ApproveStatus)
            {
                case ((int)ExpertApproveStatusEnum.Approved):
                    ApproveStatusStr = "Подтвержден";
                    break;
                case ((int)ExpertApproveStatusEnum.NotApproved):
                    ApproveStatusStr = "Не подтвержден";
                    break;
                case ((int)ExpertApproveStatusEnum.Rejected):
                    ApproveStatusStr = "Отказано";
                    break;
                case ((int)ExpertApproveStatusEnum.WaitingForApproval):
                    ApproveStatusStr = "Ожидание подтверждения";
                    break;
                default:
                    break;
            }

            LawAreaStr = "";
            foreach (var item in model.UserLawAreas)
            {
                LawAreaStr = LawAreaStr + item.LawArea.Name+ "; ";
            }

        }
    }
}
