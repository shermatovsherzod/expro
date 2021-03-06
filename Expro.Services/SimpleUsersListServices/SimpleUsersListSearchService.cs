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

namespace Expro.Services
{
    public class SimpleUsersListSearchService : ISimpleUsersListSearchService
    {
        private readonly IUserService _userService;

        public SimpleUsersListSearchService() { }

        public SimpleUsersListSearchService(IUserService userService)
        {
            _userService = userService;
        }

        public virtual IQueryable<ApplicationUser> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,        
            string authorID,
            int? regionID,
            int? cityID)
        {
            recordsTotal = 0;
            recordsFiltered = 0;
            error = "";

            try
            {
                IQueryable<ApplicationUser> simpleUsers = _userService.GetAllSimpleUsersForAdmin();

                recordsTotal = simpleUsers.Count();

                simpleUsers = ApplyFilters(simpleUsers, regionID, cityID);

                recordsFiltered = simpleUsers.Count();

                simpleUsers = ApplyOrder(simpleUsers, start, length);

                return simpleUsers;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                if (ex.InnerException != null)
                    error += ". Inner exception: " + ex.InnerException.Message;

                return Enumerable.Empty<ApplicationUser>().AsQueryable();
            }
        }

        protected IQueryable<ApplicationUser> ApplyFilters(
            IQueryable<ApplicationUser> simpleUsers,                      
            int? regionID,
            int? cityID)
        {
            if (regionID.HasValue)
            {
                simpleUsers = simpleUsers
                    .Where(m => m.RegionID == regionID);

                if (cityID.HasValue)
                {
                    simpleUsers = simpleUsers
                        .Where(m => m.CityID == cityID);
                }
            }

            return simpleUsers;
        }

        protected IQueryable<ApplicationUser> ApplyOrder(
            IQueryable<ApplicationUser> simpleUsers,
            int? start,
            int? length)
        {
            simpleUsers = simpleUsers.OrderByDescending(m => m.DateSubmittedForApproval);
            if (start.HasValue && start.Value > 0)
                simpleUsers = simpleUsers.Skip(start.Value);
            if (length.HasValue && length.Value > 0)
                simpleUsers = simpleUsers.Take(length.Value);

            return simpleUsers;
        }
    }
}