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
        IUserService _userService;
        public ExpertsListAdminActionsService()
        {

        }

        public ExpertsListAdminActionsService(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public bool Approve(ApplicationUser entity)
        {
            entity.UserStatusID = (int)ExpertApproveStatusEnum.Approved;
            entity.DateApproved = DateTime.Now;
            try
            {
                _userService.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Reject(ApplicationUser entity)
        {
            entity.UserStatusID = (int)ExpertApproveStatusEnum.Rejected;
            entity.DateApproved = DateTime.Now;          
            try
            {
                _userService.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}