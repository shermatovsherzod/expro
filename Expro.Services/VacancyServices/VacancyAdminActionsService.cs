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
    public class VacancyAdminActionsService : IVacancyAdminActionsService
    {
        protected IVacancyService _vacancyService;

        public VacancyAdminActionsService() { }

        public VacancyAdminActionsService(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        public void Approve(Vacancy entity, string userID)
        {
            entity.VacancyStatusID = (int)VacancyStatusEnum.Approved;
            entity.DateApproved = DateTime.Now;

            _vacancyService.Update(entity, userID);
        }

        public void Reject(Vacancy entity, string userID)
        {
            entity.VacancyStatusID = (int)VacancyStatusEnum.Rejected;
            entity.DateRejected = DateTime.Now;

            _vacancyService.Update(entity, userID);
        }
    }
}