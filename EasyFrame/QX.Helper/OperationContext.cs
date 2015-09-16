using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QX.Helper
{
    public class OperationContext
    {
        /// <summary>
        ///得到业务逻辑层操作工厂
        /// </summary>
        /// <returns></returns>
        public static IBLL.IBLLSessionFactory GetBLLSessionFactory()
        {
            return DI.SpringHelper.GetObject<IBLL.IBLLSessionFactory>("BLLSessionFactory");
        }


        #region 判断是否登陆
        /// <summary>
        /// 判断是否登陆
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return true;
            }
            return false;
        } 
        #endregion
    }
}
