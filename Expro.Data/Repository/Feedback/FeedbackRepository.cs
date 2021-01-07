using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class FeedbackRepository : BaseCRUDRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }
}