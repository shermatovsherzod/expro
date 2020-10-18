using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class WorkExperienceRepository : BaseCRUDRepository<WorkExperience>, IWorkExperienceRepository
    {
        public WorkExperienceRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}