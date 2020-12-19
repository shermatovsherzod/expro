using Expro.Models;
using System.Linq;

namespace Expro.Data.Repository.Interfaces
{
    public interface IUserPurchasedDocumentRepository : IBaseCRUDRepository<UserPurchasedDocument>
    {
        IQueryable<UserPurchasedDocument> GetManyWithRelatedDataAsIQueryable();
    }
}