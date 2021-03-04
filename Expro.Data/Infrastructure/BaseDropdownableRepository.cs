using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Expro.Models;
using Microsoft.EntityFrameworkCore;

namespace Expro.Data.Infrastructure
{
    public abstract class BaseDropdownableRepository<T>
        : BaseCRUDRepository<T>
        where T : BaseModelDropdownable
    {
        protected BaseDropdownableRepository(IDatabaseFactory databaseFactory)
            : base (databaseFactory)
        {
        }

        public IQueryable<T> GetWithLocalizedNameAsIQueryable()
        {
            return DbSet
                .Include(m => m.NameShort);
        }
    }
}
