using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IWorkExperienceService : IBaseAuthorableService<WorkExperience>
    {
        IQueryable<WorkExperience> GetExpertWorkExperienceByCreatorID(string userID);
        bool WorkExperienceBelongsToUser(WorkExperience model, string userID);
    }
}