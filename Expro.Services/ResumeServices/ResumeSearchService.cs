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
    public class ResumeSearchService : IResumeSearchService
    {
        protected IResumeService _resumeService;

        public ResumeSearchService() { }

        public ResumeSearchService(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        public virtual IQueryable<Resume> Search(
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
                IQueryable<Resume> resumes;

                if (curUserType.HasValue)
                {
                    if (curUserType == UserTypesEnum.Admin)
                        resumes = _resumeService.GetAllForAdmin();
                    else if (curUserType == UserTypesEnum.Expert)
                        resumes = _resumeService.GetResumeByCreatorID(authorID);                  
                    else
                        resumes = _resumeService.GetAllApproved();
                }
                else
                    resumes = _resumeService.GetAllApproved();

                recordsTotal = resumes.Count();

                resumes = ApplyFilters(resumes, statusID, regionID, cityID);

                recordsFiltered = resumes.Count();

                resumes = ApplyOrder(resumes, start, length);

                return resumes;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                if (ex.InnerException != null)
                    error += ". Inner exception: " + ex.InnerException.Message;

                return Enumerable.Empty<Resume>().AsQueryable();
            }
        }

        protected IQueryable<Resume> ApplyFilters(
            IQueryable<Resume> resumes,           
            int? statusID,
            int? regionID,
            int? cityID)
        {
            if (statusID.HasValue)
            {
                if (statusID.Value == (int)ResumeStatusEnum.Approved)
                {
                    resumes = resumes
                        .Where(m => m.ResumeStatusID == (int)ResumeStatusEnum.Approved);
                            
                }
                else if (statusID.Value == (int)ResumeStatusEnum.Rejected)
                {
                    resumes = resumes
                        .Where(m => m.ResumeStatusID == (int)ResumeStatusEnum.Rejected);

                }
                else if (statusID.Value == (int)ResumeStatusEnum.NotApproved)
                {
                    resumes = resumes
                        .Where(m => m.ResumeStatusID == (int)ResumeStatusEnum.NotApproved);

                }
                else if (statusID.Value == (int)ResumeStatusEnum.WaitingForApproval)
                {
                    resumes = resumes
                        .Where(m => m.ResumeStatusID == (int)ResumeStatusEnum.WaitingForApproval);
                }
            }

            if (regionID.HasValue)
            {
                resumes = resumes
                    .Where(m => m.RegionID == regionID);

                if (cityID.HasValue)
                {
                    resumes = resumes
                        .Where(m => m.CityID == cityID);
                }
            }

            return resumes;
        }

        protected IQueryable<Resume> ApplyOrder(
            IQueryable<Resume> resumes,
            int? start,
            int? length)
        {
            resumes = resumes.OrderByDescending(m => m.DateModified);
            if (start.HasValue && start.Value > 0)
                resumes = resumes.Skip(start.Value);
            if (length.HasValue && length.Value > 0)
                resumes = resumes.Take(length.Value);

            return resumes;
        }
    }
}