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
    public class SimpleUsersListAdminActionsService : ISimpleUsersListAdminActionsService
    {
        UserManager<ApplicationUser> _userManager;
        IUserService _userService;
        private readonly IEmailService _emailService;
        public SimpleUsersListAdminActionsService()
        {

        }

        public SimpleUsersListAdminActionsService(UserManager<ApplicationUser> userManager, IUserService userService, IEmailService emailService)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailService;
        }

        public bool ApproveEmail(ApplicationUser entity)
        {            
            entity.EmailConfirmed = true;

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