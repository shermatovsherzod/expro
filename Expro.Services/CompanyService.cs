using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Expro.Services
{
    public class CompanyService : BaseAuthorableService<Company>, ICompanyService
    {
        private ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _companyRepository = repository;
        }

        public IQueryable<Company> GetManyWithRelatedDataAsIQueryable()
        {
            return _companyRepository.GetManyWithRelatedDataAsIQueryable();
        }

        public IQueryable<Company> GetCompanyByCreatorID(string userID)
        {
            return GetManyWithRelatedDataAsIQueryable().Where(c => c.CreatedBy == userID);
        }

        public IQueryable<Company> GetAllForAdmin()
        {
            return GetAsIQueryable();
        }

        public IQueryable<Company> GetAllApproved()
        {
            return GetAsIQueryable().Where(c => c.CompanyStatusID == (int)CompanyStatusEnum.Approved);
        }

        public bool CompanyBelongsToUser(Company model, string userID)
        {
            if (model == null)
            {
                throw new System.Exception("Компания не найдена");
            }

            if (model.CreatedBy == userID)
            {
                return true;
            }
            return false;
        }

        public bool CompanyBelongsToUser(int modelID, string userID)
        {
            var model = GetByID(modelID);

            if (model == null)
            {
                throw new System.Exception("Компания не найдена");
            }

            if (model.CreatedBy == userID)
            {
                return true;
            }
            return false;
        }

        public void SubmitForApproval(Company entity, string userID)
        {
            entity.CompanyStatusID = (int)CompanyStatusEnum.WaitingForApproval;
            entity.DateSubmittedForApproval = DateTime.Now;

            Update(entity, userID);
        }
    }
}