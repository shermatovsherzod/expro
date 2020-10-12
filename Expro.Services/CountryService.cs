using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;

namespace Expro.Services
{
    public class CountryService : BaseDropdownableService<Country>, ICountryService
    {
        public CountryService(ICountryRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}