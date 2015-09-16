using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLSession:IBLL.IBLLSession
    {

        #region 得到UserBLL
        IUserBLL iUserBLL;
        public IUserBLL GetUserBLL
        {
            get
            {
                if (iUserBLL == null)
                    iUserBLL = new UserBLL();
                return iUserBLL;
            }
            set
            {
                iUserBLL = value;
            }
        } 
        #endregion
    }
}
