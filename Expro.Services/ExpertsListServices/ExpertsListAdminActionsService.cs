using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Expro.Services
{
    //public class ExpertsListAdminActionsService : IExpertsListAdminActionsService
    //{
    //    protected IUserService _userService;

    //    public ExpertsListAdminActionsService() { }

    //    public ExpertsListAdminActionsService(IUserService userService)
    //    {
    //        _userService = userService;
    //    }

    //    public void Approve(ApplicationUser entity, string userID)
    //    {
    //        entity.VacancyStatusID = (int)ExpertApproveStatusEnum.Approved;
    //        entity.DateApproved = DateTime.Now;

    //        _userService.Update(entity, userID);
    //    }

    //    public void Reject(ApplicationUser entity, string userID)
    //    {
    //        entity.VacancyStatusID = (int)ExpertApproveStatusEnum.Rejected;
    //        entity.DateRejected = DateTime.Now;

    //        _userService.Update(entity, userID);
    //    }
    //}
}