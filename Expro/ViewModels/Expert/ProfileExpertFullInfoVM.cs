using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Expro.ViewModels
{
    public class ProfileExpertFullInfoVM
    {
        public ProfileExpertFullInfoVM()
        {

        }
        public string Id { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string PatronymicName { get; set; }

        [Display(Name = "Регион")]
        public string RegionStr { get; set; }

        [Display(Name = "Город")]
        public string CityStr { get; set; }

        [Display(Name = "Другой город")]
        public string CityOther { get; set; }

        [Display(Name = "Дата рождения")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Пол")]
        public string GenderStr { get; set; }

        [Display(Name = "На сайте с:")]
        public string DateRegistered { get; set; }

        [Display(Name = "Обо мне")]
        public string AboutMe { get; set; }

        [Display(Name = "Статус")]
        public int UserStatusID { get; set; }

        [Display(Name = "Статус")]
        public string UserStatusStr { get; set; }

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
        public List<string> LawAreasName { get; set; }

        [Display(Name = "Статус")]
        public int Status { get; set; }

        public AttachmentDetailsVM Avatar { get; set; }

        public List<ExpertProfileWorkExperienceDetailsVM> WorkExperience { get; set; }

        public List<ExpertProfileEducationDetailsVM> Education { get; set; }

        public bool IsOnline { get; set; }

        public string DateApprovedStr { get; set; }
        public string DateRejectedStr { get; set; }
        public string DateSubmittedForApprovalStr { get; set; }

        public bool EmailConfirmed { get; set; }

        public ProfileExpertFullInfoVM(ApplicationUser model)
        {
            if (model == null)
                return;
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            PatronymicName = model.PatronymicName;
            CityOther = model.CityOther;
            DateOfBirth = model.DateOfBirth != DateTime.MinValue ? DateTimeUtils.ConvertToString(model.DateOfBirth, AppData.Configuration.DateViewStringFormat) : "";
            GenderStr = model.GenderID != null ? model.Gender.Name : "";
            DateRegistered = DateTimeUtils.ConvertToString(model.DateRegistered, AppData.Configuration.DateTimeViewStringFormat);
            UserStatusID = model.UserStatusID;
            UserStatusStr = model.UserStatus.Name;
            DateApprovedStr = DateTimeUtils.ConvertToString(model.DateApproved, AppData.Configuration.DateTimeViewStringFormat);
            DateRejectedStr = DateTimeUtils.ConvertToString(model.DateRejected, AppData.Configuration.DateTimeViewStringFormat);
            DateSubmittedForApprovalStr = DateTimeUtils.ConvertToString(model.DateSubmittedForApproval, AppData.Configuration.DateTimeViewStringFormat);
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
            LawAreasName = new List<string>();
            if (model.LawAreaParentID.HasValue)
                LawAreasName.Add(model.LawAreaParent.Name);
            if (model.UserLawAreas != null)
                LawAreasName.AddRange(model.UserLawAreas.Select(c => c.LawArea.Name));
            //LawAreasName = model.UserLawAreas != null ? model.UserLawAreas.Select(c => c.LawArea.Name).ToList() : null;
            Status = model.UserStatusID;
            Avatar = new AttachmentDetailsVM(model.Avatar);
            WorkExperience = new ExpertProfileWorkExperienceDetailsVM().GetListOfExpertProfileWorkExperienceDetailsVM(model.WorkExperience);
            Education = new ExpertProfileEducationDetailsVM().GetListOfExpertProfileEducationDetailsVM(model.ExpertEducations);
            IsOnline = model.IsOnline == true ? true : false;
            EmailConfirmed = model.EmailConfirmed;
        }

        public List<ProfileExpertFullInfoVM> GetListOfExpertFullInfoVM(IQueryable<ApplicationUser> models)
        {
            if (models == null)
                return new List<ProfileExpertFullInfoVM>();

            return models.Select(s => new ProfileExpertFullInfoVM
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                PatronymicName = s.PatronymicName,
                CityOther = s.CityOther,
                DateOfBirth = s.DateOfBirth != DateTime.MinValue ? DateTimeUtils.ConvertToString(s.DateOfBirth, AppData.Configuration.DateViewStringFormat) : "",
                GenderStr = s.GenderID != null ? s.Gender.Name : "",
                DateRegistered = DateTimeUtils.ConvertToString(s.DateRegistered, AppData.Configuration.DateTimeViewStringFormat),
                UserStatusID = s.UserStatusID,
                UserStatusStr = s.UserStatus.Name,
                DateApprovedStr = DateTimeUtils.ConvertToString(s.DateApproved, AppData.Configuration.DateTimeViewStringFormat),
                DateRejectedStr = DateTimeUtils.ConvertToString(s.DateRejected, AppData.Configuration.DateTimeViewStringFormat),
                DateSubmittedForApprovalStr = DateTimeUtils.ConvertToString(s.DateSubmittedForApproval, AppData.Configuration.DateTimeViewStringFormat),
                AboutMe = s.AboutMe,
                UserName = s.UserName,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                AccountNumber = s.AccountNumber,
                Balance = s.Balance,
                Fax = s.Fax,
                WebSite = s.WebSite,
                RegionStr = s.RegionID != null ? s.Region.Name : "",
                CityStr = s.CityID != null ? s.City.Name : "",
                LawAreasName = s.UserLawAreas != null ? s.UserLawAreas.Select(c => c.LawArea.Name).ToList() : null,
                Status = s.UserStatusID,
                IsOnline = s.IsOnline == true ? true : false,
                EmailConfirmed = s.EmailConfirmed
            }).ToList();
        }
    }
}
