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
    public class VacancyService : BaseAuthorableService<Vacancy>, IVacancyService
    {
        private IVacancyRepository _vacancyRepository;

        public VacancyService(IVacancyRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _vacancyRepository = repository;
        }

        public IQueryable<Vacancy> GetManyWithRelatedDataAsIQueryable()
        {
            return _vacancyRepository.GetManyWithRelatedDataAsIQueryable();
        }

        public IQueryable<Vacancy> GetVacancyByCreatorID(string userID)
        {
            return GetManyWithRelatedDataAsIQueryable().Where(c => c.CreatedBy == userID);
        }

        public IQueryable<Vacancy> GetAllForAdmin()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.VacancyStatusID != (int)VacancyStatusEnum.NotApproved);
        }

        public IQueryable<Vacancy> GetAllApproved()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(c => c.VacancyStatusID == (int)VacancyStatusEnum.Approved);
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

        public void SubmitForApproval(Vacancy entity, string userID)
        {
            entity.VacancyStatusID = (int)VacancyStatusEnum.WaitingForApproval;
            entity.DateSubmittedForApproval = DateTime.Now;

            Update(entity, userID);
        }
    }
}