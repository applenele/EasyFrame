using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLSession : IBLL.IBLLSession
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

        #region 得到PermissionBLL
        IPermissionBLL iPermissionBLL;
        public IPermissionBLL GetPermissionBLL
        {
            get
            {
                if (iPermissionBLL == null)
                    iPermissionBLL = new PermissionBLL();
                return iPermissionBLL;
            }
            set
            {
                iPermissionBLL = value;
            }
        } 
        #endregion

        #region 得到部门BLL
        IDepartmentBLL iDepartmentBLL;
        public IDepartmentBLL GetDepartmentBLL
        {
            get
            {
                if (iDepartmentBLL == null)
                    iDepartmentBLL = new DepartmentBLL();
                return iDepartmentBLL;
            }
            set
            {
                iDepartmentBLL = value;
            }
        } 
        #endregion


        #region 得到角色BLL
        IRoleBLL iRoleBLL;
        public IRoleBLL GetRoleBLL
        {
            get
            {
                if (iRoleBLL == null)
                    iRoleBLL = new RoleBLL();
                return iRoleBLL;
            }
            set
            {
                iRoleBLL = value;
            }
        } 
        #endregion

        #region 得到用户角色BLL
        IUserRoleBLL iUserRoleBLL;
        public IUserRoleBLL GetUserRoleBLL
        {
            set
            {
                iUserRoleBLL = value;
            }
            get
            {
                if (iUserRoleBLL == null)
                    iUserRoleBLL = new UserRoleBLL();
                return iUserRoleBLL;
            }
        } 
        #endregion

        #region 角色权限表
        IRolePermissionBLL iRolePermissionBLL;
        public IRolePermissionBLL GetRolePermissionBLL
        {
            get
            {
                if (iRolePermissionBLL == null)
                    iRolePermissionBLL = new RolePermissionBLL();
                return iRolePermissionBLL;
            }
            set
            {
                iRolePermissionBLL = value;
            }
        } 
        #endregion

        #region 得到特权BLL
        IUserVipPermissionBLL iUserVipPermissionBLL;
        public IUserVipPermissionBLL GetUserVipPermissionBLL
        {
            get
            {
                if (iUserVipPermissionBLL == null)
                    iUserVipPermissionBLL = new UserVipPermissionBLL();
                return iUserVipPermissionBLL;
            }
            set
            {
                iUserVipPermissionBLL = value;
            }
        } 
        #endregion

    }
}
