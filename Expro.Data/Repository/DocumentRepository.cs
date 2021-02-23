using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class DocumentRepository : BaseCRUDRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<Document> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.DocumentLawAreas)
                    .ThenInclude(m => m.LawArea)
                        .ThenInclude(m => m.NameShort)
                .Include(m => m.Creator)
                    .ThenInclude(m => m.Avatar)
                .Include(m => m.DocumentStatus)
                    .ThenInclude(m => m.NameShort);
        }

        public Document GeWithRelatedDataByID(int id)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Include(m => m.Language)
                    .ThenInclude(m => m.NameShort)
                .Include(m => m.Attachment)
                .Include(m => m.DocumentLikes)
                    .ThenInclude(m => m.Like)
                .FirstOrDefault(m => m.ID == id);
        }
    }
}