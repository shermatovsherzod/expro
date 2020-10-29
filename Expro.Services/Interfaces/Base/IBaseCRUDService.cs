using Expro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expro.Services.Interfaces
{
    public interface IBaseCRUDService<T> where T : BaseModel
    {
        void Add(T entity);
        void AddCollection(ICollection<T> entities);

        void Update(T entity);
        void UpdateCollection(ICollection<T> entities);

        void Delete(T entity);
        void DeleteCollection(ICollection<T> entities);

        //uncomment when it is need
        void DeletePermanently(T entity);
        void DeletePermanentlyCollection(ICollection<T> entities);

        T GetByID(int id);
        //T GetActiveByID(int id);
        IEnumerable<T> GetByIDs(int[] ids);
        //IEnumerable<T> GetActiveByIDs(int[] ids);
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetAllActive();
        IQueryable<T> GetAsIQueryable();
        //IQueryable<T> GetActiveAsIQueryable();
        //IQueryable<T> GetWithRelatedDataAsIQueryable();
    }
}
