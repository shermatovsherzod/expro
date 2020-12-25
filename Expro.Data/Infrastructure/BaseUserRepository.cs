using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Expro.Models;
using Microsoft.EntityFrameworkCore;

namespace Expro.Data.Infrastructure
{
    public class BaseUserRepository<T> where T : ApplicationUser
    {
        private ApplicationDbContext _dataContext;
        private readonly DbSet<T> _dbset;

        protected BaseUserRepository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected ApplicationDbContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }

        public DbSet<T> DbSet
        {
            get { return _dbset; }
        }

        public void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateCollection(ICollection<T> entities)
        {
            foreach (var p in entities)
            {
                Update(p);
            }
        }

        public virtual T GetByID(string id)
        {
            return _dbset.Find(id);
        }

        public virtual IQueryable<T> GetAsIQueryable()
        {
            return _dataContext.Set<T>();
        }
    }
}
