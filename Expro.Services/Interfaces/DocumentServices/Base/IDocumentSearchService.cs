using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IDocumentSearchService
    {
        IQueryable<Document> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,
            int? statusID,
            DocumentPriceTypesEnum? priceType,
            string authorID,
            string purchasedUserID,
            int? lawAreaParent,
            int[] lawAreas);
    }
}