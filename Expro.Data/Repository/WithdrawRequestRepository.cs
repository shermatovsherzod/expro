using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class WithdrawRequestRepository : BaseCRUDRepository<WithdrawRequest>, IWithdrawRequestRepository
    {
        public WithdrawRequestRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<WithdrawRequest> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.Creator)
                    .ThenInclude(m => m.Avatar)
                .Include(m => m.Status)
                    .ThenInclude(m => m.NameShort);
        }

        public WithdrawRequest GeWithRelatedDataByID(int id)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .FirstOrDefault(m => m.ID == id);
        }
    }
}