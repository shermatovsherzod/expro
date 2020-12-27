using Expro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expro.Data.Repository.Interfaces
{
    public interface IBaseUserRepository<T> where T : ApplicationUser
    {
        void Update(T entity);
        void UpdateCollection(ICollection<T> entities);

        T GetByID(string id);

        IQueryable<T> GetAsIQueryable();
    }
}
