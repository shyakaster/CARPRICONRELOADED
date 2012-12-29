using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Capricon.DataAccess
{
    public class ObjectContextAdapter : IObjectContext
    {
        readonly ObjectContext _context;

        public ObjectContextAdapter(ObjectContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IObjectSet<T> CreateObjectSet<T>() where T : class
        {
            return _context.CreateObjectSet<T>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
