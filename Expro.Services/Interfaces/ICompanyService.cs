using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface ICompanyService : IBaseAuthorableService<Company>
    {
        IQueryable<Company> GetCompanyByCreatorID(string userID);
        bool CompanyBelongsToUser(Company model, string userID);
        bool CompanyBelongsToUser(int modelID, string userID);
    }
}