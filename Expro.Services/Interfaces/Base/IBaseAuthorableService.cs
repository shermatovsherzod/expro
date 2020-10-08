using Expro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expro.Services.Interfaces
{
    public interface IBaseAuthorableService<T> : IBaseCRUDService<T> 
        where T : BaseModel
    {
        //void Add(T entity, string creatorID);
        //void AddCollection(ICollection<T> entities, string creatorID);

        //void Update(T entity, string modifierID);
        //void UpdateCollection(ICollection<T> entities, string modifierID);

        //void Delete(T entity, string modifierID);
        //void DeleteCollection(ICollection<T> entities, string modifierID);

        ////uncomment when it is need
        //void DeletePermanently(T entity, string modifierID);
        ////void DeletePermanentlyCollection(ICollection<T> entities, string modifierID);

        //T GetByID(int id);
        T GetActiveByID(int id);
        //IEnumerable<T> GetByIDs(int[] ids);
        IEnumerable<T> GetActiveByIDs(int[] ids);
        //IEnumerable<T> GetAll();
        IEnumerable<T> GetAllActive();
        //IQueryable<T> GetAsIQueryable();
        IQueryable<T> GetActiveAsIQueryable();
        //IQueryable<T> GetWithRelatedDataAsIQueryable();
    }
}
