using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Objects;
using Capricon.Model;
using System.Linq.Expressions;

namespace Capricon.DataAccess
{
    public class UserRepository : IRepository<User>, IDisposable
    {
        private CapriconContext context;

        public UserRepository(CapriconContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }
        public IQueryable<User> Find(Expression<Func<User, bool>> where)
        {
            return context.Users.Where(where);
        }
        public User Single(Expression<Func<User, bool>> where)
        {
            return context.Users.SingleOrDefault(where);
        }
        public User First(Expression<Func<User, bool>> where)
        {
            return context.Users.FirstOrDefault(where);
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
        }
        public void Add(User user)
        {
            context.Users.Add(user);
        }
        public void Attach(User user)
        {
            context.Entry(user).State = System.Data.EntityState.Modified;
            context.Users.Attach(user);
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
