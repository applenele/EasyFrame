using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace DAL
{
    public class DBSessionFactory :IDBSessionFactory
    {
        public IDBSession GetDBSession()
        {
           IDBSession dbsession = CallContext.GetData(typeof(DBSessionFactory).Name) as DBSession;

           if (dbsession == null)
           {
               dbsession = new DBSession();
           }

           return dbsession;
        }
    }
}
