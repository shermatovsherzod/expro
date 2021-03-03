using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IExpertsListSearchService
    {
        IQueryable<ApplicationUser> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,
            int? statusID,           
            string authorID,
            int? lawAreaParent,
            int[] lawAreas,
            int? regionID,
            int? cityID);
    }
}