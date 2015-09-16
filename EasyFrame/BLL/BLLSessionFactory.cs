using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLSessionFactory : IBLL.IBLLSessionFactory
    {
        public IBLLSession GetBLLSession()
        {
            IBLLSession dbsession = CallContext.GetData(typeof(BLLSessionFactory).Name) as BLLSession;
            if (dbsession == null)
            {
                dbsession = new BLLSession();
                CallContext.SetData(typeof(BLLSessionFactory).Name, dbsession);
            }
            return dbsession;
        }
    }
}
