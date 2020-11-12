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
                .Include(m => m.DocumentLawAreas).ThenInclude(m => m.LawArea)
                .Include(m => m.Creator)
                .Include(m => m.DocumentStatus);
        }

        public Document GeWithRelatedDataByID(int id)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Include(m => m.Language)
                .Include(m => m.Attachment)
                .FirstOrDefault(m => m.ID == id);
        }
    }
}