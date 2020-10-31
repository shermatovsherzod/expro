using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class UserPurchasedDocumentRepository : BaseCRUDRepository<UserPurchasedDocument>, IUserPurchasedDocumentRepository
    {
        public UserPurchasedDocumentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}