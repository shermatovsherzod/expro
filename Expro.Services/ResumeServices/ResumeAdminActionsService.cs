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
    public class ResumeAdminActionsService : IResumeAdminActionsService
    {
        protected IResumeService _resumesService;

        public ResumeAdminActionsService() { }

        public ResumeAdminActionsService(IResumeService resumesService)
        {
            _resumesService = resumesService;
        }

        public void Approve(Resume entity, string userID)
        {
            entity.ResumeStatusID = (int)ResumeStatusEnum.Approved;
            entity.DateApproved = DateTime.Now;

            _resumesService.Update(entity, userID);
        }

        public void Reject(Resume entity, string userID)
        {
            entity.ResumeStatusID = (int)ResumeStatusEnum.Rejected;
            entity.DateRejected = DateTime.Now;

            _resumesService.Update(entity, userID);
        }
    }
}