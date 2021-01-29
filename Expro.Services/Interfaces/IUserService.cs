using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IUserService : IBaseUserService<ApplicationUser>
    {
        bool UserIsAllowedToWorkWithPaidMaterials(ApplicationUser user);
        IQueryable<ApplicationUser> GetManyWithRelatedDataAsIQueryable();
        ApplicationUser GeWithRelatedDataByID(string id);
    }
}