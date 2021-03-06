using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface ISimpleUsersListSearchService
    {
        IQueryable<ApplicationUser> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,                  
            string authorID,            
            int? regionID,
            int? cityID);
    }
}