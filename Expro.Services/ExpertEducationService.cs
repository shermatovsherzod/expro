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

        public IQueryable<ExpertEducation> GetListByUserID(string createdBy)
        {
            return _repository.GetAsIQueryable().Where(c => c.CreatedBy == createdBy);
        }
    }
}