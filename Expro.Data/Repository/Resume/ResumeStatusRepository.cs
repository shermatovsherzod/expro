using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class ResumeStatusRepository : BaseDropdownableRepository<ResumeStatus>, IResumeStatusRepository
    {
        public ResumeStatusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}