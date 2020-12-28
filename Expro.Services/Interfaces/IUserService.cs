using Expro.Models;

namespace Expro.Services.Interfaces
{
    public interface IUserService : IBaseUserService<ApplicationUser>
    {
        bool UserIsAllowedToWorkWithPaidMaterials(ApplicationUser user);
    }
}