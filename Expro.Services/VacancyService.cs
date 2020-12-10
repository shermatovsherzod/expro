using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace Expro.Services
{
    public class VacancyService : BaseAuthorableService<Vacancy>, IVacancyService
    {
        public VacancyService(IVacancyRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IQueryable<Vacancy> GetVacancyByCreatorID(string userID)
        {
            return _repository.GetAsIQueryable().Where(c => c.CreatedBy == userID);
        }

        public bool VacancyBelongsToUser(Vacancy model, string userID)
        {
            if (model == null)
            {
                throw new System.Exception("Вакансия не найдена");
            }

            if (model.CreatedBy == userID)
            {
                return true;
            }
            return false;
        }

        public bool VacancyBelongsToUser(int modelID, string userID)
        {
            var model = _repository.GetByID(modelID);

            if (model == null)
            {
                throw new System.Exception("Вакансия не найдена");
            }

            if (model.CreatedBy == userID)
            {
                return true;
            }
            return false;
        }
    }
}