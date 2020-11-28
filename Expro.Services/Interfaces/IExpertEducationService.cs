using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IExpertEducationService : IBaseAuthorableService<ExpertEducation>
    {
        IQueryable<ExpertEducation> GetExpertEducationsByCreatorID(string userID);
        bool EducationBelongsToUser(ExpertEducation model, string userID);
    }
}