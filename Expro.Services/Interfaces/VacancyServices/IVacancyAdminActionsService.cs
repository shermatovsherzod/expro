using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IVacancyAdminActionsService
    {
        void Approve(Vacancy entity, string userID);
        void Reject(Vacancy entity, string userID);
    }
}