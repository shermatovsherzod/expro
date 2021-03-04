using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class LanguageRepository : BaseDropdownableRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}