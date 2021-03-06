using Expro.Models;
using Expro.Models.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Services.Interfaces
{
    public interface ISimpleUsersListAdminActionsService
    {  
        bool ApproveEmail(ApplicationUser entity);
    }
}