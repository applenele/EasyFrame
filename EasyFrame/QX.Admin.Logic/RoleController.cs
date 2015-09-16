using EasyUIModel;
using FomatModel;
using IBLL;
using QX.Entity;
using QX.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModel;

namespace QX.Admin.Logic
{
    public class RoleController : BaseController
    {
        public static int counter = 0;
        public ActionResult Index()
        {
            int count = 0;
            IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
            count = bll.GetListBy(r => r.IsDelete == true).Count();
            ViewBag.RecycleCount = count;

            return View();
        }


        #region 分页得到角色名称  + ActionResult GetRoles(int page, int rows)
        /// <summary>
        /// 分页得到角色名称
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetRoles(int page, int rows)
        {
            List<Role> _roles = new List<Role>();
            List<vRole> roles = new List<vRole>();
            DGModel<vRole> dgModel = new DGModel<vRole>();
            int count = 0;
            try
            {
                IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
                _roles = bll.LoadPageEntity(page, rows, out count, r => r.IsDelete == false, r => r.AddTime, false).ToList();
                foreach (var role in _roles)
                {
                    roles.Add(new vRole(role));
                }
                dgModel.rows = roles;
                dgModel.total = count;
            }
            catch
            {
                dgModel.rows = null;
                dgModel.total = count;
            }

            return Json(dgModel);
        }
        #endregion

        public ActionResult Add()
        {
            List<SelectListItem> departmentLst = new List<SelectListItem>();
            List<Department> departments = new List<Department>();
            IDepartmentBLL dbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetDepartmentBLL;

            departments = dbll.GetListBy(d => d.IsDelete == false);
            foreach (var department in departments)
            {
                departmentLst.Add(new SelectListItem { Text = department.DepartmentName, Value = department.ID.ToString() });
            }

            ViewBag.Departments = departmentLst;
            return View();
        }

        #region 执行增加权限
        /// <summary>
        /// 执行增加权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DoAdd(Role model)
        {
            AjaxModel ajaxModel = new AjaxModel();
            try
            {
                IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
                model.AddTime = DateTime.Now;
                model.IsDelete = false;
                bll.Add(model);
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "增加成功！";
                ajaxModel.BackUrl = "/Admin/Role/Index";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "增加失败！";
            }

            return Json(ajaxModel);
        }
        #endregion



        #region 修改角色
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
            Role role = new Role();
            role = bll.GetListBy(r => r.ID == id).SingleOrDefault();

            List<SelectListItem> departmentLst = new List<SelectListItem>();
            List<Department> departments = new List<Department>();
            IDepartmentBLL dbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetDepartmentBLL;
            departments = dbll.GetListBy(d => d.IsDelete == false);
            foreach (var department in departments)
            {
                departmentLst.Add(new SelectListItem { Text = department.DepartmentName, Value = department.ID.ToString(), Selected = role.DepartmentID == department.ID ? true : false });
            }
            ViewBag.Departments = departmentLst;
            ViewBag.Role = role;
            return View();
        }
        #endregion


        #region 软删除
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            AjaxModel ajaxModel = new AjaxModel();
            Role role = new Role();
            try
            {
                IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
                role = bll.GetListBy(r => r.ID == id).SingleOrDefault();
                role.IsDelete = true;
                bll.Modify(role, "IsDelete");
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "删除成功！";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "删除失败！";
            }

            return Json(ajaxModel);
        }
        #endregion

        #region 彻底删除权限
        /// <summary>
        /// 彻底删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CompleteDelete(int id)
        {
            AjaxModel ajaxModel = new AjaxModel();
            try
            {
                IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
                bll.DelBy(r => r.ID == id);
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "删除成功！";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "删除失败！";
            }
            return Json(ajaxModel);
        }
        #endregion


        #region 得到回收站里面的角色
        /// <summary>
        /// 得到回收站里面的角色
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetRoleRecycles(int page, int rows)
        {
            DGModel<vRole> dgModel = new DGModel<vRole>();
            List<Role> _roles = new List<Role>();
            List<vRole> roles = new List<vRole>();
            int count = 0;
            try
            {
                IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
                _roles = bll.LoadPageEntity(page, rows, out count, r => r.IsDelete == true, r => r.AddTime, false).ToList();

                foreach (var role in _roles)
                {
                    roles.Add(new vRole(role));
                }
                dgModel.total = count;
                dgModel.rows = roles;
            }
            catch
            {
                dgModel.total = count;
                dgModel.rows = null;
            }
            return Json(dgModel);
        }
        #endregion

        #region 还原角色
        /// <summary>
        /// 还原角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Return(int id)
        {
            AjaxModel ajaxModel = new AjaxModel();
            Role role = new Role();
            try
            {
                IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
                role = bll.GetListBy(r => r.ID == id).SingleOrDefault();
                role.IsDelete = false;
                bll.Modify(role, "IsDelete");
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "还原成功";
            }
            catch
            {
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "还原成功";
            }

            return Json(ajaxModel);
        }
        #endregion


        public ActionResult Recycle()
        {
            return View();
        }

        #region 修改角色
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DoUpdate(Role model)
        {
            AjaxModel ajaxModel = new AjaxModel();
            Role role = new Role();
            IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
            try
            {
                role = bll.GetListBy(r => r.ID == model.ID).SingleOrDefault();
                role.RoleName = model.RoleName;
                role.Remark = model.Remark;
                role.DepartmentID = model.DepartmentID;
                role.IsShow = model.IsShow;
                bll.Modify(role, "RoleName", "Remark", "DepartmentID", "IsShow");
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "修改成功";
                ajaxModel.BackUrl = "/Admin/Role/Index";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "修改失败！";
            }

            return Json(ajaxModel);
        }
        #endregion


        #region 得到角色的权限
        /// <summary>
        /// 得到角色的权限
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        public ActionResult GetRolePermissions(int rid)
        {
            List<Permission> permissions = new List<Permission>();
            List<Permission> _permissions = new List<Permission>();
            List<RolePermission> rps = new List<RolePermission>();
            List<TreeNode> permissionTree = new List<TreeNode>();
            try
            {
                IPermissionBLL pbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetPermissionBLL;
                IRolePermissionBLL rpbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRolePermissionBLL;
                rps = rpbll.GetListBy(rp => rp.RoleID == rid);
                foreach (var rp in rps)
                {
                    _permissions = pbll.GetListBy(p => p.ID == rp.PermissionID && p.IsDelete == false && p.IsShow == true);
                    foreach (var permission in _permissions)
                    {
                        permissions.Add(permission);
                    }
                }

                permissionTree = Permission.ToTreeNodes(permissions);
            }
            catch
            {
            }
            return Json(permissionTree);
        }
        #endregion


        /// <summary>
        /// 得到权限无授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetPermissionsToAuthorize(int rid)
        {
            List<TreeNode> permissionTree = new List<TreeNode>();
            // List<Permission> permissions = new List<Permission>();
            List<Permission> _permissions = new List<Permission>();
            List<RolePermission> rps = new List<RolePermission>();
            List<Permission> allPermission = new List<Permission>();
            List<string> owner = new List<string>();


            IPermissionBLL pbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetPermissionBLL;
            IRolePermissionBLL rpbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRolePermissionBLL;
            rps = rpbll.GetListBy(rp => rp.RoleID == rid);
            allPermission = pbll.GetListBy(p => p.IsDelete == false && p.IsShow == true);
            foreach (var rp in rps)
            {
                _permissions = pbll.GetListBy(p => p.ID == rp.PermissionID && p.IsDelete == false && p.IsShow == true);
                foreach (var permission in _permissions)
                {
                    //permissions.Add(permission);
                    owner.Add(permission.ID.ToString());
                }
            }
            permissionTree = Permission.ToTreeNodes(allPermission);
            ToCheck(permissionTree, owner);
            return Json(permissionTree);
        }


        public void ToCheck(List<TreeNode> nodes, List<string> ids)
        {
            foreach (var node in nodes)
            {
                if (ids.Contains(node.id))
                {
                    node.Checked = true;
                }
                if (node.children != null)
                {
                    foreach (var n in node.children)
                    {
                        if (n.sondata == "")
                        {
                            node.sondata = n.id;
                        }
                        else
                        {
                            node.sondata = "," + n.id;
                        }
                    }
                    ToCheck(node.children, ids);
                }
            }
            counter++;
        }

        #region 角色授权
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <param name="authorties"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        public ActionResult Authorize(string authorties, int rid)
        {
            AjaxModel ajaxModel = new AjaxModel();
            authorties = authorties.Substring(0, authorties.Length - 1);
            List<RolePermission> rps = new List<RolePermission>();
            try
            {
                IRolePermissionBLL rpbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRolePermissionBLL;
                rpbll.DelBy(rp => rp.RoleID == rid);
                string[] authorizeIDString = authorties.Split(',');
                foreach (var id in authorizeIDString)
                {
                    rpbll.Add(new RolePermission { PermissionID = int.Parse(id), RoleID = rid, IsDelete = false, AddTime = DateTime.Now, });
                }
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "授权成功";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "授权失败";
            }
            return Json(ajaxModel);
        }
        #endregion


        #region 根据名称查找角色
        /// <summary>
        /// 根据名称查找角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult GetRoleByRoleName(string name)
        {
            AjaxModel ajaxModel = new AjaxModel();
            List<Role> roles = new List<Role>();
            string key = name.Trim().ToString();
            try
            {
                IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
                if (string.IsNullOrEmpty(name))
                {
                    roles = bll.GetListBy(r => r.IsDelete == false && r.IsShow == true);
                }
                else
                {
                    roles = bll.GetListBy(r => r.IsDelete == false && r.IsShow == true && r.RoleName.Contains(key.Trim()));
                }
                ajaxModel.Data = roles;
                ajaxModel.Statu = "ok";
            }
            catch
            {
                ajaxModel.Data = roles;
                ajaxModel.Statu = "err";
            }
            return Json(ajaxModel, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 根据名称查找角色
        /// <summary>
        /// 根据和用户名称查找角色 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult GetRoleByRoleNameByUser(string name,int uid)
        {
            AjaxModel ajaxModel = new AjaxModel();
            List<Role> roles = new List<Role>();
            string key = name.Trim().ToString();
            User user = new User();
            try
            {
                IRoleBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
                IUserBLL ubll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserBLL;
                user = ubll.GetListBy(u => u.ID == uid).SingleOrDefault();
                if (string.IsNullOrEmpty(name))
                {
                    roles = bll.GetListBy(r => r.IsDelete == false && r.IsShow == true && r.DepartmentID==user.DepartmentID);
                }
                else
                {
                    roles = bll.GetListBy(r => r.IsDelete == false && r.IsShow == true && r.RoleName.Contains(key.Trim()) && r.DepartmentID==user.DepartmentID);
                }
                ajaxModel.Data = roles;
                ajaxModel.Statu = "ok";
            }
            catch
            {
                ajaxModel.Data = roles;
                ajaxModel.Statu = "err";
            }
            return Json(ajaxModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
