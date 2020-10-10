using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class GenderRepository : BaseCRUDRepository<Gender>, IGenderRepository
    {
        public GenderRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}