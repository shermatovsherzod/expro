using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class RegionRepository : BaseDropdownableRepository<Region>, IRegionRepository
    {
        public RegionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}