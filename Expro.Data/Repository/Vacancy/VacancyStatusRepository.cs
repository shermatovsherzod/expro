using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class VacancyStatusRepository : BaseCRUDRepository<VacancyStatus>, IVacancyStatusRepository
    {
        public VacancyStatusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}