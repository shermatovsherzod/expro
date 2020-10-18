using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IWorkExperienceService : IBaseCRUDService<WorkExperience>
    {
        IQueryable<WorkExperience> GetListByUserID(string userID);
    }
}