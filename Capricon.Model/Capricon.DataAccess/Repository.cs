using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Objects;
using System.Data.Entity;
using Capricon.Model;

namespace Capricon.DataAccess
{
    public class Repository<T> where T : class
    {
        internal CapriconContext context;
        internal DbSet<T> dbSet;

        public Repository(CapriconContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual T Find(Expression<Func<T, bool>> where)
        {
            return dbSet.Find(where);
        }

        public virtual T Single(Expression<Func<T, bool>> where)
        {
            return dbSet.SingleOrDefault(where);
        }
      
        public virtual T First(Expression<Func<T, bool>> where)
        {
            return dbSet.FirstOrDefault(where);
        }
      
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
     
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }
    
        public virtual void Attach(T entity)
        {
            context.Entry(entity).State = System.Data.EntityState.Modified;
            dbSet.Attach(entity);
        }
    }
}
