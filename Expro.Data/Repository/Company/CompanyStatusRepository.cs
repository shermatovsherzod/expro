using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class CompanyStatusRepository : BaseDropdownableRepository<CompanyStatus>, ICompanyStatusRepository
    {
        public CompanyStatusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}