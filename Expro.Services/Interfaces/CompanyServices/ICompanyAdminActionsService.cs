using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface ICompanyAdminActionsService
    {
        void Approve(Company entity, string userID);
        void Reject(Company entity, string userID);
    }
}