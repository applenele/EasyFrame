using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDBSession
    {
        IUserDAL GetUserDAL { set; get; }

        IPermissionDAL GetPermissionDAL { set; get; }

        IDepartmentDAL GetDepartmentDAL { set; get; }

        IRoleDAL GetRoleDAL { set; get; }

        IUserRoleDAL GetUserRoleDAL { set; get; }

        IRolePermissionDAL GetRolePermissionDAL { set; get; }

        IUserVipPermissionDAL GetUserVipPermissionDAL { set; get; }
    }

}


