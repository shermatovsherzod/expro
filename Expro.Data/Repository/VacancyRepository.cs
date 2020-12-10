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

    }
}