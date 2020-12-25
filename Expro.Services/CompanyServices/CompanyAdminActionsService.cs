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
    public class CompanyAdminActionsService : ICompanyAdminActionsService
    {
        protected ICompanyService _companyService;

        public CompanyAdminActionsService() { }

        public CompanyAdminActionsService(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public void Approve(Company entity, string userID)
        {
            entity.CompanyStatusID = (int)CompanyStatusEnum.Approved;
            entity.DateApproved = DateTime.Now;

            _companyService.Update(entity, userID);
        }

        public void Reject(Company entity, string userID)
        {
            entity.CompanyStatusID = (int)CompanyStatusEnum.Rejected;
            entity.DateRejected = DateTime.Now;

            _companyService.Update(entity, userID);
        }
    }
}