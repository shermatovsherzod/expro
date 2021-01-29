using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class CompanyRepository : BaseCRUDRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<Company> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.Logo)
                .Include(m => m.CompanyLawAreas)
                    .ThenInclude(m => m.LawArea)
                .Include(m => m.Creator)
                .Include(m => m.Region)
                .Include(m => m.City)
                .Include(m => m.CompanyStatus);
        }
    }
}