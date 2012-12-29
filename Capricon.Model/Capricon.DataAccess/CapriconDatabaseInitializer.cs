using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Capricon.Model
{
    public class CapriconDatabaseInitializer : DropCreateDatabaseIfModelChanges<CapriconContext>
    {
        public CapriconDatabaseInitializer()
        {

        }

    }
}
