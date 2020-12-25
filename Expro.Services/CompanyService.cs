using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace Expro.Services
{
    public class CompanyService : BaseAuthorableService<Company>, ICompanyService
    {
        public CompanyService(ICompanyRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IQueryable<Company> GetCompanyByCreatorID(string userID)
        {
            return _repository.GetAsIQueryable().Where(c => c.CreatedBy == userID);
        }

        public IQueryable<Company> GetAllForAdmin()
        {
            return _repository.GetAsIQueryable();
        }

        public IQueryable<Company> GetAllApproved()
        {
            return _repository.GetAsIQueryable().Where(c => c.CompanyStatusID == (int)CompanyStatusEnum.Approved);
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
            var model = _repository.GetByID(modelID);

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
    }
}