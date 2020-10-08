using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;

namespace Expro.Services
{
    public class CityService : BaseDropdownableService<City>, ICityService
    {
        public CityService(ICityRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}