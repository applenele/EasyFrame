using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBLLSession
    {
        IUserBLL GetUserBLL { set; get; }

        IPermissionBLL GetPermissionBLL { set; get; }

        IDepartmentBLL GetDepartmentBLL { set; get; }

        IRoleBLL GetRoleBLL { set; get; }

        IUserRoleBLL GetUserRoleBLL { set; get; }

        IRolePermissionBLL GetRolePermissionBLL { set; get; }

        IUserVipPermissionBLL GetUserVipPermissionBLL { set; get; }
    }
}
