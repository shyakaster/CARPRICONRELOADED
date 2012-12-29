using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Capricon.DataAccess
{
    public interface IObjectContext : IDisposable
    {
        IObjectSet<T> CreateObjectSet<T>() where T : class;
            void SaveChanges();
    }
}
