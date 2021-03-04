using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class DocumentStatusRepository : BaseDropdownableRepository<DocumentStatus>, IDocumentStatusRepository
    {
        public DocumentStatusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}