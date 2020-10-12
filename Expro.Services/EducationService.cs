using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class EducationService : BaseCRUDService<Education>, IEducationService
    {
        public EducationService(IEducationRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IQueryable<Education> GetListByUserID(string userID)
        {
            return _repository.GetAsIQueryable().Where(c => c.UserID == userID);
        }
    }
}