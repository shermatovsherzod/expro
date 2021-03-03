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
    public class VacancySearchService : IVacancySearchService
    {
        protected IVacancyService _vacancyService;

        public VacancySearchService() { }

        public VacancySearchService(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        public virtual IQueryable<Vacancy> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,
            int? statusID,          
            string authorID,
            int? regionID,
            int? cityID)
        {
            recordsTotal = 0;
            recordsFiltered = 0;
            error = "";

            try
            {
                IQueryable<Vacancy> vacancies;

                if (curUserType.HasValue)
                {
                    if (curUserType == UserTypesEnum.Admin)
                        vacancies = _vacancyService.GetAllForAdmin();
                    else if (curUserType == UserTypesEnum.Expert || curUserType == UserTypesEnum.SimpleUser)
                        vacancies = _vacancyService.GetVacancyByCreatorID(authorID);                  
                    else
                        vacancies = _vacancyService.GetAllApproved();
                }
                else
                    vacancies = _vacancyService.GetAllApproved();

                recordsTotal = vacancies.Count();

                vacancies = ApplyFilters(vacancies, statusID, regionID, cityID);

                recordsFiltered = vacancies.Count();

                vacancies = ApplyOrder(vacancies, start, length);

                return vacancies;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                if (ex.InnerException != null)
                    error += ". Inner exception: " + ex.InnerException.Message;

                return Enumerable.Empty<Vacancy>().AsQueryable();
            }
        }

        protected IQueryable<Vacancy> ApplyFilters(
            IQueryable<Vacancy> vacancies,           
            int? statusID,
            int? regionID,
            int? cityID)
        {
            if (statusID.HasValue)
            {
                if (statusID.Value == (int)VacancyStatusEnum.Approved)
                {
                    vacancies = vacancies
                        .Where(m => m.VacancyStatusID == (int)VacancyStatusEnum.Approved);
                            
                }
                else if (statusID.Value == (int)VacancyStatusEnum.Rejected)
                {
                    vacancies = vacancies
                        .Where(m => m.VacancyStatusID == (int)VacancyStatusEnum.Rejected);

                }
                else if (statusID.Value == (int)VacancyStatusEnum.NotApproved)
                {
                    vacancies = vacancies
                        .Where(m => m.VacancyStatusID == (int)VacancyStatusEnum.NotApproved);

                }
                else if (statusID.Value == (int)VacancyStatusEnum.WaitingForApproval)
                {
                    vacancies = vacancies
                        .Where(m => m.VacancyStatusID == (int)VacancyStatusEnum.WaitingForApproval);
                }
            }

            if (regionID.HasValue)
            {
                vacancies = vacancies
                    .Where(m => m.RegionID == regionID);

                if (cityID.HasValue)
                {
                    vacancies = vacancies
                        .Where(m => m.CityID == cityID);
                }
            }

            return vacancies;
        }

        protected IQueryable<Vacancy> ApplyOrder(
            IQueryable<Vacancy> vacancies,
            int? start,
            int? length)
        {
            vacancies = vacancies.OrderByDescending(m => m.DateModified);
            if (start.HasValue && start.Value > 0)
                vacancies = vacancies.Skip(start.Value);
            if (length.HasValue && length.Value > 0)
                vacancies = vacancies.Take(length.Value);

            return vacancies;
        }
    }
}