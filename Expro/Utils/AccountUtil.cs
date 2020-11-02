using Expro.Models;
using Expro.Models.Enums;
//using Expro.Models.Enums;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Expro.Utils
{
    public class AccountUtil
    {
        public AppUserVM GetCurrentUser(ClaimsPrincipal user)
        {
            return new AppUserVM(user);
        }

        //public static bool IsUserCustomer(AppUserVM userVM)
        //{
        //    return userVM.Type == UserTypesEnum.customer.ToString();
        //}

        //public static bool IsUserTalent(AppUserVM userVM)
        //{
        //    return userVM.Type == UserTypesEnum.talent.ToString();
        //}
    }

    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {

        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(CustomClaimTypes.FirstName, user.FirstName)
                });
            }
            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(CustomClaimTypes.LastName, user.LastName)
                });
            }

            if (!string.IsNullOrWhiteSpace(user.PatronymicName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(CustomClaimTypes.PatronymicName, user.PatronymicName)
                });
            }

            if (!string.IsNullOrWhiteSpace(user.UserName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(CustomClaimTypes.UserName, user.UserName)
                });
            }

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(CustomClaimTypes.Email, user.Email)
                });
            }

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(CustomClaimTypes.PhoneNumber, user.PhoneNumber)
                });
            }

            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(CustomClaimTypes.UserType, ((int)user.UserType).ToString())
            });

            //switch (user.UserType)
            //{
            //    case 1:
            //        ((ClaimsIdentity)principal.Identity).AddClaims(new[] { new Claim(CustomClaimTypes.UserType, "Admin") });
            //        break;
            //    case 2:
            //        ((ClaimsIdentity)principal.Identity).AddClaims(new[] { new Claim(CustomClaimTypes.UserType, "Expert") });
            //        break;
            //    case 3:
            //        ((ClaimsIdentity)principal.Identity).AddClaims(new[] { new Claim(CustomClaimTypes.UserType, "SimpleUser") });
            //        break;
            //    default:
            //        ((ClaimsIdentity)principal.Identity).AddClaims(new[] { new Claim(CustomClaimTypes.UserType, "") });
            //        break;
            //}  


            //if (!string.IsNullOrWhiteSpace(user.CityOther))
            //{
            //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            //        new Claim(CustomClaimTypes.CityOther, user.CityOther)
            //    });
            //}


            //if (user.DateOfBirth != DateTime.MinValue)
            //{
            //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            //        new Claim(CustomClaimTypes.DateOfBirth, user.DateOfBirth.ToString())
            //    });
            //}

            //if (user.GenderID.GetValueOrDefault(0) != 0)
            //{
            //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            //        new Claim(CustomClaimTypes.GenderID, user.GenderID.ToString())
            //    });
            //}

            //if (user.RegionID.GetValueOrDefault(0) != 0)
            //{
            //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            //        new Claim(CustomClaimTypes.RegionID, user.RegionID.ToString())
            //    });
            //}

            //if (user.CityID.GetValueOrDefault(0) != 0)
            //{
            //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            //        new Claim(CustomClaimTypes.CityID, user.CityID.ToString())
            //    });
            //}


            //if (!string.IsNullOrWhiteSpace(user.UserType))
            //{
            //    ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
            //        new Claim(CustomClaimTypes.UserType, user.UserType),
            //        new Claim(CustomClaimTypes.FirebaseUid, user.FirebaseUid),
            //        //new Claim(CustomClaimTypes.EmailConfirmed, user.EmailConfirmed.ToString()),
            //        //new Claim(CustomClaimTypes.TalentApprovedByAdmin, user.TalentApprovedByAdmin.ToString())
            //    });
            //}

            return principal;
        }
    }

    public static class CustomClaimTypes
    {
        public const string UserType = "UserType";
        public const string FirebaseUid = "FirebaseUid";

        public const string UserName = "UserName";
        public const string Email = "Email";
        public const string PhoneNumber = "PhoneNumber";
        public const string DateOfBirth = "DateOfBirth";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string PatronymicName = "PatronymicName";
        public const string GenderID = "GenderID";
        public const string CityID = "CityID";
        public const string CityOther = "CityOther";
        public const string RegionID = "RegionID";

        //public const string EmailConfirmed = "EmailConfirmed";
        //public const string TalentApprovedByAdmin = "TalentApprovedByAdmin";
    }
}
