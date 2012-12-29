using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Objects;
using Capricon.Model;
using System.Linq.Expressions;

namespace Capricon.DataAccess
{
    public class AgentMessageRepository : IRepository<AgentMessage>, IDisposable
    {
        private CapriconContext context;

        public AgentMessageRepository(CapriconContext context)
        {
            this.context = context;
        }

        public IEnumerable<AgentMessage> GetAll()
        {
            return context.AgentMessages.ToList();
        }
        public IQueryable<AgentMessage> Find(Expression<Func<AgentMessage, bool>> where)
        {
            return context.AgentMessages.Where(where);
        }
        public AgentMessage Single(Expression<Func<AgentMessage, bool>> where)
        {
            return context.AgentMessages.SingleOrDefault(where);
        }
        public AgentMessage First(Expression<Func<AgentMessage, bool>> where)
        {
            return context.AgentMessages.FirstOrDefault(where);
        }

        public void Delete(AgentMessage agentMessage)
        {
            context.AgentMessages.Remove(agentMessage);
        }
        public void Add(AgentMessage agentMessage)
        {
            context.AgentMessages.Add(agentMessage);
        }
        public void Attach(AgentMessage agentMessage)
        {
            context.Entry(agentMessage).State = System.Data.EntityState.Modified;
            context.AgentMessages.Attach(agentMessage);
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
