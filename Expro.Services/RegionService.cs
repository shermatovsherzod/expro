using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;

namespace Expro.Services
{
    public class RegionService : BaseDropdownableService<Region>, IRegionService
    {
        public RegionService(IRegionRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}