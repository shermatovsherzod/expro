using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class DocumentAnswerRepository : BaseCRUDRepository<DocumentAnswer>, IDocumentAnswerRepository
    {
        public DocumentAnswerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}