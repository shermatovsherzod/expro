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
    public class ResumeService : BaseAuthorableService<Resume>, IResumeService
    {
        private IResumeRepository _resumeRepository;

        public ResumeService(IResumeRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            _resumeRepository = repository;
        }

        public IQueryable<Resume> GetManyWithRelatedDataAsIQueryable()
        {
            return _resumeRepository.GetManyWithRelatedDataAsIQueryable();
        }

        public IQueryable<Resume> GetResumeByCreatorID(string userID)
        {
            return GetManyWithRelatedDataAsIQueryable().Where(c => c.CreatedBy == userID);
        }

        public IQueryable<Resume> GetAllForAdmin()
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.ResumeStatusID != (int)ResumeStatusEnum.NotApproved);
        }

        public IQueryable<Resume> GetAllApproved()
        {
            return GetManyWithRelatedDataAsIQueryable().Where(c => c.ResumeStatusID == (int)ResumeStatusEnum.Approved);
        }

        public bool ResumeBelongsToUser(Resume model, string userID)
        {
            if (model == null)
            {
                throw new System.Exception("Резюме не найден");
            }

            if (model.CreatedBy == userID)
            {
                return true;
            }
            return false;
        }

        public bool ResumeBelongsToUser(int modelID, string userID)
        {
            var model = _repository.GetByID(modelID);

            if (model == null)
            {
                throw new System.Exception("Резюме не найден");
            }

            if (model.CreatedBy == userID)
            {
                return true;
            }
            return false;
        }

        public void SubmitForApproval(Resume entity, string userID)
        {
            entity.ResumeStatusID = (int)ResumeStatusEnum.WaitingForApproval;
            entity.DateSubmittedForApproval = DateTime.Now;

            Update(entity, userID);
        }
    }
}