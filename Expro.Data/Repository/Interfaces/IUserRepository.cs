using Expro.Models;
using System.Linq;

namespace Expro.Data.Repository.Interfaces
{
    public interface IUserRepository : IBaseUserRepository<ApplicationUser>
    {
        IQueryable<ApplicationUser> GetManyWithRelatedDataAsIQueryable();
    }
}