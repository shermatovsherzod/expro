using Expro.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class AppUserVM
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

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
    }
}
