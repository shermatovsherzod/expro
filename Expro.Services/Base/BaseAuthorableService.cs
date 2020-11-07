using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Infrastructure = Expro.Data.Infrastructure;

namespace Expro.Services
{
    public class BaseAuthorableService<T> : BaseCRUDService<T> where T : BaseModelAuthorable
    {
        //protected IBaseCRUDRepository<T> _repository { get; set; }

        //protected Infrastructure.IUnitOfWork _unitOfWork { get; set; }

        public BaseAuthorableService(IBaseCRUDRepository<T> repository, Infrastructure.IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            //this._repository = repository;
            //this._unitOfWork = unitOfWork;
        }

        public virtual void Add(T entity, string creatorID)
        {
            OnPreUpdate(entity, creatorID);
            base.Add(entity);
        }

        public void AddCollection(ICollection<T> entities, string creatorID)
        {
            foreach (var item in entities)
            {
                OnPreUpdate(item, creatorID);
            }

            base.AddCollection(entities);
        }

        public void Update(T entity, string modifierID)
        {
            OnPreUpdate(entity, modifierID);
            base.Update(entity);
        }

        public void UpdateCollection(ICollection<T> entities, string modifierID)
        {
            foreach (var item in entities)
            {
                OnPreUpdate(item, modifierID);
            }
            
            base.UpdateCollection(entities);
        }

        public void Delete(T entity, string modifierID)
        {
            entity.IsDeleted = true;
            OnPreUpdate(entity, modifierID);

            Update(entity);
        }

        public void DeleteCollection(ICollection<T> entities, string modifierID)
        {
            foreach (var item in entities)
            {
                item.IsDeleted = true;
            }

            UpdateCollection(entities);
        }

        //public override void DeletePermanently(T entity)
        //{
        //    base.DeletePermanently(entity);
        //}

        //public override void DeletePermanentlyCollection(ICollection<T> entities)
        //{
        //    base.DeletePermanentlyCollection(entities);
        //}

        //public virtual T GetByID(int id)
        //{
        //    return _repository.GetByID(id);
        //}

        public virtual T GetActiveByID(int id)
        {
            T entity = GetByID(id);
            if (entity != null)
                return entity.IsDeleted ? null : entity;

            return entity;
        }

        //public virtual IEnumerable<T> GetByIDs(int[] ids)
        //{
        //    if (ids == null || ids.Length == 0)
        //        return new List<T>();

        //    return GetAsIQueryable()
        //        .Where(m => ids.Contains(m.ID));
        //}

        public virtual IEnumerable<T> GetActiveByIDs(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return new List<T>();

            return GetAsIQueryable()
                .Where(m => ids.Contains(m.ID) && !m.IsDeleted);
        }

        //public virtual IEnumerable<T> GetAll()
        //{
        //    return _repository.GetAll();
        //}

        public virtual IEnumerable<T> GetAllActive()
        {
            return GetAsIQueryable().Where(m => !m.IsDeleted);
        }

        //public virtual IQueryable<T> GetAsIQueryable()
        //{
        //    return _repository.GetAsIQueryable();
        //}

        //public virtual IQueryable<T> GetAllActiveAsIQueryable()
        //{
        //    return GetAsIQueryable().Where(m => !m.IsDeleted);
        //}

        //public virtual IQueryable<T> GetWithRelatedDataAsIQueryable()
        //{
        //    return _repository.GetWithRelatedDataAsIQueryable().Where(m => !m.IsDeleted);
        //}

        public virtual IQueryable<T> GetActiveAsIQueryable()
        {
            return GetAsIQueryable().Where(m => !m.IsDeleted);
        }

        void OnPreUpdate(T entity, string userID)
        {
            DateTime now = DateTime.Now;

            if (entity.ID == 0)
            {
                entity.CreatedBy = userID;
                entity.DateCreated = now;
                entity.IsDeleted = false;
            }

            entity.ModifiedBy = userID;
            entity.DateModified = now;
        }

        //void Save()
        //{
        //    _unitOfWork.Commit();
        //}
    }
}
