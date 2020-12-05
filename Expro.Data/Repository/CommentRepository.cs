using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class CommentRepository : BaseCRUDRepository<Comment>, ICommentRepository
    {
        public CommentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}