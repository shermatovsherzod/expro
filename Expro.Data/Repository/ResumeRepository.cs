using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class ResumeRepository : BaseCRUDRepository<Resume>, IResumeRepository
    {
        public ResumeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<Resume> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.Creator)
                .Include(m => m.Region)
                .Include(m => m.City)
                .Include(m => m.ResumeStatus);
        }
    }
}