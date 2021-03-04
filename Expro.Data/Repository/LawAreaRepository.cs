using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class LawAreaRepository : BaseDropdownableRepository<LawArea>, ILawAreaRepository
    {
        public LawAreaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}