using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Objects;
using Capricon.Model;
using System.Linq.Expressions;

namespace Capricon.DataAccess
{
    public class PaymentRepository : IRepository<Payment>, IDisposable
    {
        private CapriconContext context;

        public PaymentRepository(CapriconContext context)
        {
            this.context = context;
        }

        public IEnumerable<Payment> GetAll()
        {
            return context.Payments.ToList();
        }
        public IQueryable<Payment> Find(Expression<Func<Payment, bool>> where)
        {
            return context.Payments.Where(where);
        }
        public Payment Single(Expression<Func<Payment, bool>> where)
        {
            return context.Payments.SingleOrDefault(where);
        }
        public Payment First(Expression<Func<Payment, bool>> where)
        {
            return context.Payments.FirstOrDefault(where);
        }

        public void Delete(Payment payment)
        {
            context.Payments.Remove(payment);
        }
        public void Add(Payment payment)
        {
            context.Payments.Add(payment);
        }
        public void Attach(Payment payment)
        {
            context.Entry(payment).State = System.Data.EntityState.Modified;
            context.Payments.Attach(payment);
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
