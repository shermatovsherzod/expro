using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class ClickTransactionRepository : BaseCRUDRepository<ClickTransaction>, IClickTransactionRepository
    {
        public ClickTransactionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}