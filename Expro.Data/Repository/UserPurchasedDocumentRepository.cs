using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class UserPurchasedDocumentRepository : BaseCRUDRepository<UserPurchasedDocument>, IUserPurchasedDocumentRepository
    {
        public UserPurchasedDocumentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<UserPurchasedDocument> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.Document).ThenInclude(m => m.DocumentType)
                .Include(m => m.Document).ThenInclude(m => m.Creator)
                .Include(m => m.User);
        }
    }
}