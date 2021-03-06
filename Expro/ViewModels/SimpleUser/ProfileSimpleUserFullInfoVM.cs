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

namespace Expro.ViewModels
{
    public class ProfileSimpleUserFullInfoVM
    {

        public ProfileSimpleUserFullInfoVM() { }

        public string Id { get; set; }

        [Display(Name = "lblFirstName", ResourceType = typeof(ResourceTexts))]
        public string FirstName { get; set; }

        [Display(Name = "lblLastName", ResourceType = typeof(ResourceTexts))]
        public string LastName { get; set; }

        [Display(Name = "lblPatronymicName", ResourceType = typeof(ResourceTexts))]
        public string PatronymicName { get; set; }

        [Display(Name = "lblRegion", ResourceType = typeof(ResourceTexts))]
        public string RegionStr { get; set; }

        [Display(Name = "lblCity", ResourceType = typeof(ResourceTexts))]
        public string CityStr { get; set; }

        [Display(Name = "lblCityOther", ResourceType = typeof(ResourceTexts))]
        public string CityOther { get; set; }

        [Display(Name = "lblDateOfBirth", ResourceType = typeof(ResourceTexts))]
        public string DateOfBirth { get; set; }

        [Display(Name = "lblGender", ResourceType = typeof(ResourceTexts))]
        public string GenderStr { get; set; }

        [Display(Name = "lblDateRegistered", ResourceType = typeof(ResourceTexts))]
        public string DateRegistered { get; set; }

        [Display(Name = "Email", ResourceType = typeof(ResourceTexts))]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }
        public AttachmentDetailsVM Avatar { get; set; }
        public bool IsOnline { get; set; }
        public ProfileSimpleUserFullInfoVM(ApplicationUser model)
        {
            if (model == null)
                return;
            Id = model.Id;
            FirstName = model.FirstName;
            LastName = model.LastName;
            PatronymicName = model.PatronymicName;
            CityOther = model.CityOther;
            DateOfBirth = DateTimeUtils.ConvertToString(model.DateOfBirth);
            GenderStr = model.Gender != null ? model.Gender.Name : "";
            RegionStr = model.Region != null ? model.Region.Name : "";
            CityStr = model.City != null ? model.City.Name : "";
            DateRegistered = DateTimeUtils.ConvertToString(model.DateRegistered);
            Email = model.Email;
            EmailConfirmed = model.EmailConfirmed;
            Avatar = new AttachmentDetailsVM(model.Avatar);
            IsOnline = model.IsOnline == true ? true : false;
        }
    }
}
