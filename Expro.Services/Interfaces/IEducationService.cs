using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IEducationService : IBaseCRUDService<Education>
    {
        IQueryable<Education> GetListByUserID(string userID);
    }
}