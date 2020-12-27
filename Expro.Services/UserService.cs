﻿using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;

namespace Expro.Services
{
    public class UserService : BaseUserService<ApplicationUser>, IUserService
    {
        public UserService(IUserRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
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