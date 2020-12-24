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
    public class CompanySearchService : ICompanySearchService
    {
        protected ICompanyService _companyService;

        public CompanySearchService() { }

        public CompanySearchService(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public virtual IQueryable<Company> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,
            int? statusID,          
            string authorID
            )
        {
            recordsTotal = 0;
            recordsFiltered = 0;
            error = "";

            try
            {
                IQueryable<Company> companies;

                if (curUserType.HasValue)
                {
                    if (curUserType == UserTypesEnum.Admin)
                        companies = _companyService.GetAllForAdmin();
                    else if (curUserType == UserTypesEnum.Expert)
                        companies = _companyService.GetCompanyByCreatorID(authorID);                  
                    else
                        companies = _companyService.GetAllApproved();
                }
                else
                    companies = _companyService.GetAllApproved();

                recordsTotal = companies.Count();

                companies = ApplyFilters(companies, statusID);

                recordsFiltered = companies.Count();

                companies = ApplyOrder(companies, start, length);

                return companies;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                if (ex.InnerException != null)
                    error += ". Inner exception: " + ex.InnerException.Message;

                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        protected IQueryable<Company> ApplyFilters(
            IQueryable<Company> companies,           
            int? statusID
           )
        {
            if (statusID.HasValue)
            {
                if (statusID.Value == (int)CompanyStatusEnum.Approved)
                {
                    companies = companies
                        .Where(m => m.CompanyStatusID == (int)CompanyStatusEnum.Approved);
                            
                }
                else if (statusID.Value == (int)CompanyStatusEnum.Rejected)
                {
                    companies = companies
                        .Where(m => m.CompanyStatusID == (int)CompanyStatusEnum.Rejected);

                }
                else if (statusID.Value == (int)CompanyStatusEnum.NotApproved)
                {
                    companies = companies
                        .Where(m => m.CompanyStatusID == (int)CompanyStatusEnum.NotApproved);

                }
                else if (statusID.Value == (int)CompanyStatusEnum.WaitingForApproval)
                {
                    companies = companies
                        .Where(m => m.CompanyStatusID == (int)CompanyStatusEnum.WaitingForApproval);
                }
            }          

            return companies;
        }

        protected IQueryable<Company> ApplyOrder(
            IQueryable<Company> companies,
            int? start,
            int? length)
        {
            companies = companies.OrderByDescending(m => m.DateModified);
            if (start.HasValue && start.Value > 0)
                companies = companies.Skip(start.Value);
            if (length.HasValue && length.Value > 0)
                companies = companies.Take(length.Value);

            return companies;
        }
    }
}