using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Objects;
using Capricon.Model;
using System.Linq.Expressions;

namespace Capricon.DataAccess
{
    public class AgentRepository : IRepository<Agent>, IDisposable
    {
        private CapriconContext context;

        public AgentRepository(CapriconContext context)
        {
            this.context = context;
        }

        public IEnumerable<Agent> GetAll()
        {
            return context.Agents.ToList();
        }
        public IQueryable<Agent> Find(Expression<Func<Agent, bool>> where)
        {
            return context.Agents.Where(where);
        }
        public Agent Single(Expression<Func<Agent, bool>> where)
        {
            return context.Agents.SingleOrDefault(where);
        }
        public Agent First(Expression<Func<Agent, bool>> where)
        {
            return context.Agents.FirstOrDefault(where);
        }

        public void Delete(Agent agent)
        {
            context.Agents.Remove(agent);
        }
        public void Add(Agent agent)
        {
            context.Agents.Add(agent);
        }
        public void Attach(Agent agent)
        {
            context.Entry(agent).State = System.Data.EntityState.Modified;
            context.Agents.Attach(agent);
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
