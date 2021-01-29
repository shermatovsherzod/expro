using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace Expro.Services
{
    public class WorkExperienceService : BaseAuthorableService<WorkExperience>, IWorkExperienceService
    {
        public WorkExperienceService(IWorkExperienceRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IQueryable<WorkExperience> GetExpertWorkExperienceByUserID(string userID)
        {
            return _repository.GetAsIQueryable().Where(c => c.UserID == userID);
        }

        public bool WorkExperienceBelongsToUser(WorkExperience model, string userID)
        {
            if (model.UserID == userID)
            {              
                return true;
            }
            return false;
        }
    }
}