using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class CityRepository : BaseDropdownableRepository<City>, ICityRepository
    {
        public CityRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}