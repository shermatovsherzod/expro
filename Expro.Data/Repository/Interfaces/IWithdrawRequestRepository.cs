using Expro.Models;
using System.Linq;

namespace Expro.Data.Repository.Interfaces
{
    public interface IWithdrawRequestRepository : IBaseCRUDRepository<WithdrawRequest>
    {
        IQueryable<WithdrawRequest> GetManyWithRelatedDataAsIQueryable();
        WithdrawRequest GeWithRelatedDataByID(int id);
    }
}