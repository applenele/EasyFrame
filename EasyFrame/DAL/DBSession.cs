using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBSession : IDBSession
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

        #region 得到Permission数据操作仓库
        IPermissionDAL iPermissionDAL;
        public IPermissionDAL GetPermissionDAL
        {
            get
            {
                if (iPermissionDAL == null)
                    iPermissionDAL = new PermissionDAL();
                return iPermissionDAL;
            }
            set
            {
                iPermissionDAL = value;
            }
        }
        #endregion

        #region 得到部门DAL
        IDepartmentDAL iDepartmentDAL;
        public IDepartmentDAL GetDepartmentDAL
        {
            get
            {
                if (iDepartmentDAL == null)
                    iDepartmentDAL = new DepartmentDAL();
                return iDepartmentDAL;
            }
            set
            {
                iDepartmentDAL = value;
            }
        }
        #endregion

        #region 角色DAL
        IRoleDAL iRoleDAL;
        public IRoleDAL GetRoleDAL
        {
            get
            {
                if (iRoleDAL == null)
                    iRoleDAL = new RoleDAL();
                return iRoleDAL;
            }
            set
            {
                iRoleDAL = value;
            }
        }
        #endregion


        #region UserRoleDAL
        IUserRoleDAL iUserRoleDAL;
        public IUserRoleDAL GetUserRoleDAL
        {
            get
            {
                if (iUserRoleDAL == null)
                    iUserRoleDAL = new UserRoleDAL();
                return iUserRoleDAL;
            }
            set
            {
                iUserRoleDAL = value;
            }
        }
        #endregion

        #region 角色权限DAL
        IRolePermissionDAL iRolePermissionDAL;
        public IRolePermissionDAL GetRolePermissionDAL
        {
            get
            {
                if (iRolePermissionDAL == null)
                    iRolePermissionDAL = new RolePermissionDAL();
                return iRolePermissionDAL;
            }
            set
            {
                iRolePermissionDAL = value;
            }
        }
        #endregion

        #region 用户特权DAL
        IUserVipPermissionDAL iUserVipPermissionDAL;
        public IUserVipPermissionDAL GetUserVipPermissionDAL
        {
            get
            {
                if (iUserVipPermissionDAL == null)
                    iUserVipPermissionDAL = new UserVipPermissionDAL();
                return iUserVipPermissionDAL;
            }
            set
            {
                iUserVipPermissionDAL = value;
            }
        } 
        #endregion
    }
}
