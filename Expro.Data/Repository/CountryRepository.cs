﻿using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class CountryRepository : BaseDropdownableRepository<Country>, ICountryRepository
    {
        public CountryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}