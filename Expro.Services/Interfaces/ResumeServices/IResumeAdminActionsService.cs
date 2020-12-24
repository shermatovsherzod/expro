using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IResumeAdminActionsService
    {
        void Approve(Resume entity, string userID);
        void Reject(Resume entity, string userID);
    }
}