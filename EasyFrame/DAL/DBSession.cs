using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBSession:IDBSession
    {
        #region UserDAL  数据访问接口
        IUserDAL iUserDAL;
        public IUserDAL GetUserDAL
        {
            get
            {
                if (iUserDAL == null)
                    iUserDAL = new UserDAL();
                return iUserDAL;
            }
            set
            {
                iUserDAL = value;
            }
        }
        #endregion
    }
}
