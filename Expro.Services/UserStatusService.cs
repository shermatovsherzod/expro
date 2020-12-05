using Expro.Common;
using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Services
{
    public class UserStatusService : IUserStatusService
    {
        UserManager<ApplicationUser> _userManager;

        public UserStatusService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public UserStatus GetUserStatus(ApplicationUser user)
        {
            UserStatus userStatus = new UserStatus();

            return userStatus;
        }

        public async Task<bool> Approve(string userID)
        {
            ApplicationUser user = _userManager.Users.FirstOrDefault(c => c.Id == userID);

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.ApproveStatus = (int)ExpertApproveStatusEnum.Approved;
            user.DateApproved = DateTime.Now;
            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Reject(string userID)
        {
            ApplicationUser user= _userManager.Users.FirstOrDefault(c => c.Id == userID);

            if (user == null)         
                throw new Exception("Пользователь не найден");
         
            user.ApproveStatus = (int)ExpertApproveStatusEnum.Rejected;
            user.DateRejected = DateTime.Now;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ProfileConfirmationRequestByExpert(string userID)
        {
            ApplicationUser user = _userManager.Users.FirstOrDefault(c => c.Id == userID);

            if (user == null)
                throw new Exception("Пользователь не найден");

            user.ApproveStatus = (int)ExpertApproveStatusEnum.WaitingForApproval;
            user.DateSubmittedForApproval = DateTime.Now;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}