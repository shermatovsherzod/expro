using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class AttachmentRepository : BaseCRUDRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}