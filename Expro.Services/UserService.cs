using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IQueryable<ApplicationUser> GetAsIQueryable()
        {
            return _userManager.Users;
        }

        public ApplicationUser GetByID(string id)
        {
            return GetAsIQueryable().FirstOrDefault(m => m.Id == id);
        }

        public IQueryable<ApplicationUser> GetAllForAdmin()
        {
            return GetAsIQueryable();
        }

        public IQueryable<ApplicationUser> GetAllApproved()
        {
            return GetAsIQueryable().Where(c => c.UserStatusID == (int)ExpertApproveStatusEnum.Approved);
        }        
    }
}