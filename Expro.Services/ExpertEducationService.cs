using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class ExpertEducationService : BaseAuthorableService<ExpertEducation>, IExpertEducationService
    {
        public ExpertEducationService(IExpertEducationRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IQueryable<ExpertEducation> GetExpertEducationsByUserID(string userID)
        {
            return _repository.GetAsIQueryable().Where(c => c.UserID == userID);
        }

        public bool EducationBelongsToUser(ExpertEducation model, string userID)
        {
            if (model.UserID == userID)
            {               
                return true;
            }
            return false;
        }
    }
}