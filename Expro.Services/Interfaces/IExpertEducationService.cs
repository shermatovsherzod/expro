using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IExpertEducationService : IBaseAuthorableService<ExpertEducation>
    {
        IQueryable<ExpertEducation> GetListByUserID(string userID);
    }
}