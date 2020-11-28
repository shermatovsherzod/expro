using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class ExpertEducationRepository : BaseCRUDRepository<ExpertEducation>, IExpertEducationRepository
    {
        public ExpertEducationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}