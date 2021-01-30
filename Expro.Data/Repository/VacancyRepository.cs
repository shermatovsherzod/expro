using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class VacancyRepository : BaseCRUDRepository<Vacancy>, IVacancyRepository
    {
        public VacancyRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<Vacancy> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.Creator)
                .Include(m => m.Region)
                .Include(m => m.City)
                .Include(m => m.VacancyStatus);
        }
    }
}