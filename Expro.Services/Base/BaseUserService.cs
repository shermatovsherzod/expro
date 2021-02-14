using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Infrastructure = Expro.Data.Infrastructure;

namespace Expro.Services
{
    public class BaseUserService<T> where T : ApplicationUser
    {
        protected IBaseUserRepository<T> _repository { get; set; }

        protected Infrastructure.IUnitOfWork _unitOfWork { get; set; }

        public BaseUserService(IBaseUserRepository<T> repository, Infrastructure.IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        //public virtual void Add(T entity)
        //{
        //    _repository.Add(entity);
        //    Save();
        //}

        //public virtual void AddCollection(ICollection<T> entities)
        //{
        //    _repository.AddCollection(entities);
        //    Save();
        //}

        public virtual void Update(T entity)
        {
            _repository.Update(entity);
            Save();
        }

        public virtual void UpdateCollection(ICollection<T> entities)
        {
            _repository.UpdateCollection(entities);
            Save();
        }

        //public virtual void Delete(T entity)
        //{
        //    Update(entity);
        //}

        //public virtual void DeleteCollection(ICollection<T> entities)
        //{
        //    //foreach (var item in entities)
        //    //{
        //    //    item.IsDeleted = true;
        //    //}

        //    UpdateCollection(entities);
        //}

        //public virtual void DeletePermanently(T entity)
        //{
        //    _repository.Delete(entity);
        //    Save();
        //}

        //public virtual void DeletePermanentlyCollection(ICollection<T> entities)
        //{
        //    _repository.DeleteCollection(entities);
        //    Save();
        //}

        public virtual T GetByID(string id)
        {
            return _repository.GetByID(id);
        }

        ////public virtual T GetActiveByID(int id)
        ////{
        ////    T entity = GetByID(id);
        ////    if (entity != null)
        ////        return entity.IsDeleted ? null : entity;

        ////    return entity;
        ////}

        //public virtual IEnumerable<T> GetByIDs(int[] ids)
        //{
        //    if (ids == null || ids.Length == 0)
        //        return new List<T>();

        //    return GetAsIQueryable()
        //        .Where(m => ids.Contains(m.ID));
        //}

        ////public virtual IEnumerable<T> GetActiveByIDs(int[] ids)
        ////{
        ////    if (ids == null || ids.Length == 0)
        ////        return new List<T>();

        ////    return GetAsIQueryable()
        ////        .Where(m => ids.Contains(m.ID) && !m.IsDeleted);
        ////}

        //public virtual IEnumerable<T> GetAll()
        //{
        //    return _repository.GetAll();
        //}

        ////public virtual IEnumerable<T> GetAllActive()
        ////{
        ////    return GetAsIQueryable().Where(m => !m.IsDeleted);
        ////}

        public virtual IQueryable<T> GetAsIQueryable()
        {
            return _repository.GetAsIQueryable();
        }

        ////public virtual IQueryable<T> GetAllActiveAsIQueryable()
        ////{
        ////    return GetAsIQueryable().Where(m => !m.IsDeleted);
        ////}

        ////public virtual IQueryable<T> GetWithRelatedDataAsIQueryable()
        ////{
        ////    return _repository.GetWithRelatedDataAsIQueryable().Where(m => !m.IsDeleted);
        ////}

        ////public virtual IQueryable<T> GetActiveAsIQueryable()
        ////{
        ////    return GetAsIQueryable().Where(m => !m.IsDeleted);
        ////}

        ////void OnPreUpdate(T entity, string userID)
        ////{
        ////    if (entity.ID == 0)
        ////    {
        ////        entity.CreatedBy = userID;
        ////        entity.DateCreated = DateTime.Now;
        ////        entity.IsDeleted = false;
        ////    }

        ////    entity.ModifiedBy = userID;
        ////    entity.DateModified = DateTime.Now;
        ////}

        void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
