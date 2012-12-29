using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Objects;
using Capricon.Model;
using System.Linq.Expressions;

namespace Capricon.DataAccess
{
    public class UserMessageRepository : IRepository<UserMessage>, IDisposable
    {
        private CapriconContext context;

        public UserMessageRepository(CapriconContext context)
        {
            this.context = context;
        }

        public IEnumerable<UserMessage> GetAll()
        {
            return context.UserMessages.ToList();
        }
        public IQueryable<UserMessage> Find(Expression<Func<UserMessage, bool>> where)
        {
            return context.UserMessages.Where(where);
        }
        public UserMessage Single(Expression<Func<UserMessage, bool>> where)
        {
            return context.UserMessages.SingleOrDefault(where);
        }
        public UserMessage First(Expression<Func<UserMessage, bool>> where)
        {
            return context.UserMessages.FirstOrDefault(where);
        }

        public void Delete(UserMessage userMessage)
        {
            context.UserMessages.Remove(userMessage);
        }
        public void Add(UserMessage userMessage)
        {
            context.UserMessages.Add(userMessage);
        }
        public void Attach(UserMessage userMessage)
        {
            context.Entry(userMessage).State = System.Data.EntityState.Modified;
            context.UserMessages.Attach(userMessage);
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
