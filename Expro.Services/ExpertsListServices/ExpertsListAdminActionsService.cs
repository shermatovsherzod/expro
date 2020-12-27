using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Expro.Services
{
    public class ExpertsListAdminActionsService : IExpertsListAdminActionsService
    {
        UserManager<ApplicationUser> _userManager;

        public ExpertsListAdminActionsService() { }

        public ExpertsListAdminActionsService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Approve(ApplicationUser entity)
        {
            ApplicationUser user = _userManager.Users.FirstOrDefault(c => c.Id == entity.Id);

            entity.UserStatusID = (int)ExpertApproveStatusEnum.Approved;
            entity.DateApproved = DateTime.Now;
            try
            {
                IdentityResult result = await _userManager.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                var ss = ex;
            }
            

            //if (result.Succeeded)
            //{
            //    return true;
            //}
            return false;
        }

        public async Task<bool> Reject(ApplicationUser entity)
        {
            entity.UserStatusID = (int)ExpertApproveStatusEnum.Rejected;
            entity.DateApproved = DateTime.Now;
            IdentityResult result = await _userManager.UpdateAsync(entity);

            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }      
    }
}