using Expro.Models;
using Expro.Models.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Services.Interfaces
{
    public interface IExpertsListAdminActionsService
    {
        Task<bool> Approve(ApplicationUser entity);
        Task<bool> Reject(ApplicationUser entity);
    }
}