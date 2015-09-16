using QX.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserVipPermissionBLL:BaseBLL<UserVipPermission> ,IBLL.IUserVipPermissionBLL
    {
        public override void SetDAL()
        {
            idal = DBSession.GetUserVipPermissionDAL;
        }
    }
}
