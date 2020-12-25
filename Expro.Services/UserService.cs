using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;

namespace Expro.Services
{
    public class UserService : BaseUserService<ApplicationUser>, IUserService
    {
        public UserService(IUserRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}