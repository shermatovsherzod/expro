using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class UserStatusRepository : BaseDropdownableRepository<UserStatus>, IUserStatusRepository
    {
        public UserStatusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}