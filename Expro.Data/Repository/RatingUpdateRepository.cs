using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class RatingUpdateRepository : BaseCRUDRepository<RatingUpdate>, IRatingUpdateRepository
    {
        public RatingUpdateRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}