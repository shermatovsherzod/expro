using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace Expro.Services
{
    public class ResumeService : BaseAuthorableService<Resume>, IResumeService
    {
        public ResumeService(IResumeRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IQueryable<Resume> GetResumeByCreatorID(string userID)
        {
            return _repository.GetAsIQueryable().Where(c => c.CreatedBy == userID);
        }

        public IQueryable<Resume> GetAllForAdmin()
        {
            return _repository.GetAsIQueryable();
        }

        public IQueryable<Resume> GetAllApproved()
        {
            return _repository.GetAsIQueryable().Where(c => c.ResumeStatusID == (int)ResumeStatusEnum.Approved);
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
    }
}