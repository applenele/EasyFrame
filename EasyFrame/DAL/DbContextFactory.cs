using QX.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbContextFactory
    {
        public DB GetDbContext()
        {
            DB dbcontext = CallContext.GetData(typeof(DbContextFactory).Name) as DB;
            if (dbcontext == null)
            {
                dbcontext = new DB();
                CallContext.SetData(typeof(DbContextFactory).Name, dbcontext);
            }
            return dbcontext;
        }
    }
}
