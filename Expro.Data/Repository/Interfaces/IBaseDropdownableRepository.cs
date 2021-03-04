using Expro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expro.Data.Repository.Interfaces
{
    public interface IBaseDropdownableRepository<T> : IBaseCRUDRepository<T>
        where T : BaseModelDropdownable
    {
        IQueryable<T> GetWithLocalizedNameAsIQueryable();
    }
}
