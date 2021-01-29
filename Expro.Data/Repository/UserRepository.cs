using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class UserRepository : BaseUserRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<ApplicationUser> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.Avatar);
        }

        public IQueryable<ApplicationUser> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.WorkExperience)
                .Include(m => m.ExpertEducations);
        }

        public ApplicationUser GeWithRelatedDataByID(string id)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Include(m => m.WorkExperience)
                .Include(m => m.ExpertEducations)
                .FirstOrDefault(m => m.Id == id);
        }
    }
}