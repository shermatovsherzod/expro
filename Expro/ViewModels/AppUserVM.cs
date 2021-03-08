using Expro.Common;
using Expro.Common.Utilities;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Resources;
using Expro.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class AppUserVM
    {
        public string ID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblFirstName", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ResourceTexts))]
        [Display(Name = "lblLastName", ResourceType = typeof(ResourceTexts))]
        [StringLength(256)]
        public string LastName { get; set; }
        public string UserName { get; set; }

        [Display(Name = "Имя")]
        public string FullName 
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Email { get; set; }
        //public string UserType { get; set; }

        [Display(Name = "Пользователь")]
        public UserTypesEnum? UserType { get; set; }
        public UserAreasEnum? UserArea { get; set; }

        public bool IsSimpleUser
        {
            get
            {
                return UserType.HasValue && UserType.Value == UserTypesEnum.SimpleUser;
            }
        }

        public bool IsExpert
        {
            get
            {
                return UserType.HasValue && UserType.Value == UserTypesEnum.Expert;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return UserType.HasValue && UserType.Value == UserTypesEnum.Admin;
            }
        }

        [Display(Name = "Баланс")]
        public int Balance { get; set; }
        public string BalanceStr { get; set; }

        public bool IsOnline { get; set; }
        public string DateLastSeen { get; set; }

        [Display(Name = "Балл")]
        public int Points { get; set; }
        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }

        public string DateRatingLastUpdated { get; set; }

        public AttachmentDetailsVM Avatar { get; set; }

        //public string PhoneNumber { get; set; }
        //public DateTime DateOfBirth { get; set; }     
        //public string PatronymicName { get; set; }
        //public int? GenderID { get; set; }
        //public int? CityID { get; set; }
        //public string CityOther { get; set; }
        //public int? RegionID { get; set; }

        //public string Type { get; set; }
        //public string FirebaseUid { get; set; }
        //public bool EmailConfirmed { get; set; } = false;
        //public bool TalentApprovedByAdmin { get; set; }

        public AppUserVM() { }

        public AppUserVM(ClaimsPrincipal user)
        {
            if (user == null || !user.Identity.IsAuthenticated)
                return;

            ID = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            FirstName = user.FindFirst(CustomClaimTypes.FirstName).Value;
            LastName = user.FindFirst(CustomClaimTypes.LastName).Value;
            UserName = user.FindFirst(CustomClaimTypes.UserName).Value;
            Email = user.FindFirst(CustomClaimTypes.Email).Value;
            //UserType= user.FindFirst(CustomClaimTypes.UserType).Value;
            int userTypeInt = 0;
            var userTypeClaim = user.FindFirst(CustomClaimTypes.UserType);
            if (userTypeClaim != null && (!string.IsNullOrWhiteSpace(userTypeClaim.Value)))
                int.TryParse(userTypeClaim.Value, out userTypeInt);
            if (Enum.IsDefined(typeof(UserTypesEnum), userTypeInt))
                UserType = (UserTypesEnum)userTypeInt;

            if (UserType.HasValue)
            {
                switch (UserType.Value)
                {
                    case (UserTypesEnum.Admin):
                        UserArea = UserAreasEnum.Admin;
                        break;
                    case (UserTypesEnum.Expert):
                        UserArea = UserAreasEnum.Expert;
                        break;
                    case (UserTypesEnum.SimpleUser):
                        UserArea = UserAreasEnum.User;
                        break;
                    default:
                        break;
                }
            }

            //PhoneNumber = user.FindFirst(CustomClaimTypes.PhoneNumber).Value;       
            //PatronymicName = user.FindFirst(CustomClaimTypes.PatronymicName).Value;        
            //CityOther = user.FindFirst(CustomClaimTypes.CityOther).Value;

            //int regionID;
            //int.TryParse(user.FindFirst(CustomClaimTypes.RegionID)?.Value, out regionID);
            //RegionID = regionID;

            //int cityID;
            //int.TryParse(user.FindFirst(CustomClaimTypes.CityID)?.Value, out cityID);
            //CityID = cityID;

            //int genderID;
            //int.TryParse(user.FindFirst(CustomClaimTypes.GenderID)?.Value, out genderID);
            //GenderID = genderID;

            //DateTime dateOfBirth;
            //DateTime.TryParse(user.FindFirst(CustomClaimTypes.DateOfBirth)?.Value, out dateOfBirth);
            //DateOfBirth = dateOfBirth;

            //Type = user.FindFirst(CustomClaimTypes.UserType)?.Value;
            //FirebaseUid = user.FindFirst(CustomClaimTypes.FirebaseUid)?.Value;

            //bool tmpBool = false;
            //bool.TryParse(user.FindFirst(CustomClaimTypes.EmailConfirmed)?.Value, out tmpBool);
            //EmailConfirmed = tmpBool;

            //tmpBool = false;
            //bool.TryParse(user.FindFirst(CustomClaimTypes.TalentApprovedByAdmin)?.Value, out tmpBool);
            //TalentApprovedByAdmin = tmpBool;
        }

        public AppUserVM(ApplicationUser user)
        {
            if (user == null)
                return;

            ID = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = !string.IsNullOrWhiteSpace(user.Email) ? user.Email : "";
            UserType = user.UserType;
            Balance = user.Balance;
            BalanceStr = Balance.ToString(AppData.Configuration.NumberViewStringFormat);
            IsOnline = user.IsOnline ?? false;
            DateLastSeen = DateTimeUtils.ConvertToString(
                user.DateLastSeen,
                AppData.Configuration.DateTimeViewStringFormat);

            Points = user.Points;
            Rating = user.Rating;
            DateRatingLastUpdated = DateTimeUtils.ConvertToString(
                user.DateRatingLastUpdated,
                AppData.Configuration.DateTimeViewStringFormat);

            Avatar = new AttachmentDetailsVM(user.Avatar);
        }
    }

    public class ExpertDetailsVM : AppUserVM
    {
        public string AboutMe { get; set; }
        //...

        public ExpertDetailsVM() { }
        
        public ExpertDetailsVM(ApplicationUser user)
            : base(user)
        {
            if (user == null)
                return;

            AboutMe = user.AboutMe;
        }
    }
}
