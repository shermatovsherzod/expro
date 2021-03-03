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
    public class ExpertsListSearchService : IExpertsListSearchService
    {
        private readonly IUserService _userService;

        public ExpertsListSearchService() { }

        public ExpertsListSearchService(IUserService userService)
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
            int? statusID,
            string authorID,
            int? lawAreaParent,
            int[] lawAreas,
            int? regionID,
            int? cityID)
        {
            recordsTotal = 0;
            recordsFiltered = 0;
            error = "";

            try
            {
                IQueryable<ApplicationUser> experts;

                if (curUserType.HasValue)
                {
                    if (curUserType == UserTypesEnum.Admin)
                        experts = _userService.GetAllExpertsForAdmin();
                    else
                        experts = _userService.GetAllApprovedExperts();
                }
                else
                    experts = _userService.GetAllApprovedExperts();

                recordsTotal = experts.Count();

                experts = ApplyFilters(experts, statusID, lawAreaParent, lawAreas, regionID, cityID);

                recordsFiltered = experts.Count();

                experts = ApplyOrder(experts, start, length);

                return experts;
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
            IQueryable<ApplicationUser> experts,
            int? statusID,
            int? lawAreaParent,
            int[] lawAreas,
            int? regionID,
            int? cityID)
        {
            if (statusID.HasValue)
            {
                experts = experts
                    .Where(m => m.UserStatusID == statusID.Value);
                //if (statusID.Value == (int)ExpertApproveStatusEnum.Approved)
                //{
                //    experts = experts
                //        .Where(m => m.UserStatusID == (int)ExpertApproveStatusEnum.Approved);
                //}
                //else if (statusID.Value == (int)ExpertApproveStatusEnum.Rejected)
                //{
                //    experts = experts
                //        .Where(m => m.UserStatusID == (int)ExpertApproveStatusEnum.Rejected);
                //}
                //else if (statusID.Value == (int)ExpertApproveStatusEnum.NotApproved)
                //{
                //    experts = experts
                //        .Where(m => m.UserStatusID == (int)ExpertApproveStatusEnum.NotApproved);
                //}
                //else if (statusID.Value == (int)ExpertApproveStatusEnum.WaitingForApproval)
                //{
                //    experts = experts
                //        .Where(m => m.UserStatusID == (int)ExpertApproveStatusEnum.WaitingForApproval);
                //}
            }

            if (lawAreaParent.HasValue)
            {
                experts = experts
                    .Where(m => m.LawAreaParentID == lawAreaParent);

                if (lawAreas != null && lawAreas.Length > 0)
                {
                    experts = experts
                        .Where(m => m.UserLawAreas
                            .Select(n => n.LawAreaID)
                            .Any(n => lawAreas.Contains(n)));
                }
            }

            if (regionID.HasValue)
            {
                experts = experts
                    .Where(m => m.RegionID == regionID);

                if (cityID.HasValue)
                {
                    experts = experts
                        .Where(m => m.CityID == cityID);
                }
            }

            return experts;
        }

        protected IQueryable<ApplicationUser> ApplyOrder(
            IQueryable<ApplicationUser> experts,
            int? start,
            int? length)
        {
            experts = experts.OrderByDescending(m => m.DateSubmittedForApproval);
            if (start.HasValue && start.Value > 0)
                experts = experts.Skip(start.Value);
            if (length.HasValue && length.Value > 0)
                experts = experts.Take(length.Value);

            return experts;
        }
    }
}