using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class EducationRepository : BaseCRUDRepository<Education>, IEducationRepository
    {
        public EducationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}