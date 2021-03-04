using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class FeedbackStatusRepository : BaseDropdownableRepository<FeedbackStatus>, IFeedbackStatusRepository
    {
        public FeedbackStatusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}