using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Objects;
using Capricon.Model;
using System.Linq.Expressions;

namespace Capricon.DataAccess
{
    public class MessageRepository : IRepository<Message>, IDisposable
    {
        private CapriconContext context;

        public MessageRepository(CapriconContext context)
        {
            this.context = context;
        }

        public IEnumerable<Message> GetAll()
        {
            return context.Messages.ToList();
        }
        public IQueryable<Message> Find(Expression<Func<Message, bool>> where)
        {
            return context.Messages.Where(where);
        }
        public Message Single(Expression<Func<Message, bool>> where)
        {
            return context.Messages.SingleOrDefault(where);
        }
        public Message First(Expression<Func<Message, bool>> where)
        {
            return context.Messages.FirstOrDefault(where);
        }

        public void Delete(Message message)
        {
            context.Messages.Remove(message);
        }
        public void Add(Message message)
        {
            context.Messages.Add(message);
        }
        public void Attach(Message message)
        {
            context.Entry(message).State = System.Data.EntityState.Modified;
            context.Messages.Attach(message);
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
