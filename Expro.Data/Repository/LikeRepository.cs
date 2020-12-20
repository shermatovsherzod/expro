using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class LikeRepository : BaseCRUDRepository<Like>, ILikeRepository
    {
        public LikeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}