using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EasyUIModel;
using ViewModel;
using IBLL;
using QX.Helper;
using QX.Entity;
using FomatModel;
using QX.Entity.Models;

namespace QX.Admin.Logic
{
    public class PermissionController : BaseController
    {
        public ActionResult Index()
        {
            IPermissionBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetPermissionBLL;
            int count = bll.GetListBy(p => p.IsDelete == true, p => p.AddTime).Count();
            ViewBag.RecycleCount = count;
            return View();
        }


        #region 分页得到权限列表
        /// <summary>
        /// 分页得到权限列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPermissions(int page, int rows)
        {
            DGModel<vPermission> dgModel = new DGModel<vPermission>();
            List<Permission> permissions = new List<Permission>();
            List<vPermission> _permissions = new List<vPermission>();
            try
            {
                IPermissionBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetPermissionBLL;

                // permissions = bll.GetPagedList(0, 10, p => p.IsDelete == false,p=>p.AddTime);
                int count = 0;//= bll.GetListBy(p => p.PName == p.PName).ToList().Count();
                permissions = bll.LoadPageEntity(page, rows, out count, p => p.IsDelete == false, p => p.AddTime, false).ToList();

                foreach (var permission in permissions)
                {
                    _permissions.Add(new vPermission(permission));
                }
                dgModel.rows = _permissions;
                dgModel.total = count;
            }
            catch
            {
                dgModel.total = 0;
                dgModel.rows = null;
            }

            return Json(dgModel);
        }
        #endregion


        #region 得到树形权限列表
        /// <summary>
        ///得到树形权限列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetPermissions1()
        {
            List<Permission> permissions = new List<Permission>();
            List<TreeNode> permissionTree = new List<TreeNode>();
            try
            {
                IPermissionBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetPermissionBLL;
                permissions =bll.GetListBy(p => p.IsDelete == false && p.IsShow == true, p => p.AddTime).ToList();
                permissionTree= Permission.ToTreeNodes(permissions);
            }
            catch
            {
            }
            return Json(permissionTree);
        }
        #endregion

        public ActionResult Add(int fid)
        {
            List<SelectListItem> formLst = new List<SelectListItem>();
            formLst.Add(new SelectListItem { Text = "POST", Value = "0", Selected = true });
            formLst.Add(new SelectListItem { Text = "GET", Value = "1", Selected = false });

            ViewBag.FormLst = formLst;
            ViewBag.FID = (fid == 0 ? null : fid.ToString());
            return View();
        }


        #region 增加权限
        /// <summary>
        /// 增加权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoAdd(Permission model)
        {
            AjaxModel ajaxModel = new AjaxModel();

            try
            {
                IPermissionBLL bll = DI.SpringHelper.GetObject<IBLL.IBLLSessionFactory>("BLLSessionFactory").GetBLLSession().GetPermissionBLL;
                model.AddTime = DateTime.Now;
                model.IsDelete = false;
                bll.Add(model);
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "添加成功！";
                ajaxModel.BackUrl = "/Admin/Permission/Index";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "添加失败！";
            }
            return Json(ajaxModel);
        }
        #endregion

        #region 删除权限
        /// <summary>
        /// 软删除权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            AjaxModel ajaxModel = new AjaxModel();
            try
            {
                IPermissionBLL bll = DI.SpringHelper.GetObject<IBLL.IBLLSessionFactory>("BLLSessionFactory").GetBLLSession().GetPermissionBLL;
                Permission permission = new Permission();
                permission = bll.GetListBy(p => p.ID == id).SingleOrDefault();
                permission.IsDelete = true;
                bll.Modify(permission, "IsDelete");

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

        #region 修改权限
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            IPermissionBLL bll = DI.SpringHelper.GetObject<IBLL.IBLLSessionFactory>("BLLSessionFactory").GetBLLSession().GetPermissionBLL;
            Permission permission = new Permission();
            permission = bll.GetListBy(p => p.ID == id).SingleOrDefault();
            List<SelectListItem> formLst = new List<SelectListItem>();
            formLst.Add(new SelectListItem { Text = "POST", Value = "0", Selected = permission.PFormMethod == 0 ? true : false });
            formLst.Add(new SelectListItem { Text = "GET", Value = "1", Selected = permission.PFormMethod == 1 ? true : false });

            ViewBag.FormLst = formLst;

            ViewBag.Permission = permission;
            return View();
        }
        #endregion

        #region 修改权限
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DoUpdate(Permission model)
        {
            AjaxModel ajaxModel = new AjaxModel();
            try
            {
                IPermissionBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetPermissionBLL;
                bll.Modify(model, "PName", "PAreaName", "PControllerName", "PActionName", "PFormMethod", "PURL", "IsShow", "Remark");
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "增加成功！";
                ajaxModel.BackUrl = "/Admin/Permission/Index";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "增加失败！";
            }
            return Json(ajaxModel);
        }
        #endregion


        #region 显示回收站的权限
        /// <summary>
        ///  显示回收站的权限
        /// </summary>
        /// <returns></returns>
        public ActionResult Recycle()
        {
            return View();
        }
        #endregion


        #region 分页得到回收的权限
        /// <summary>
        /// 分页得到回收的权限
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetRecycles(int page, int rows)
        {
            DGModel<vPermission> dgModel = new DGModel<vPermission>();
            List<Permission> _permissions = new List<Permission>();
            List<vPermission> permissions = new List<vPermission>();
            int count = 0;

            try
            {
                IPermissionBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetPermissionBLL;
                _permissions = bll.LoadPageEntity(page, rows, out count, p => p.IsDelete == true, p => p.AddTime, false).ToList();
                foreach (var permission in _permissions)
                {
                    permissions.Add(new vPermission(permission));
                }

                dgModel.total = count;
                dgModel.rows = permissions;
            }
            catch
            {
                dgModel.total = count;
                dgModel.rows = null;
            }

            return Json(dgModel);
        }
        #endregion




        #region 将回收箱 的权限还原
        /// <summary>
        /// 将回收箱 的权限还原
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Return(int id)
        {
            AjaxModel ajaxModle = new AjaxModel();
            Permission permission = new Permission();
            try
            {
                IPermissionBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetPermissionBLL;
                permission = bll.GetListBy(p => p.ID == id).SingleOrDefault();
                permission.IsDelete = false;
                bll.Modify(permission, "IsDelete");
                ajaxModle.Statu = "ok";
                ajaxModle.Msg = "还原成功";
            }
            catch
            {
                ajaxModle.Statu = "err";
                ajaxModle.Msg = "还原失败";
            }
            return Json(ajaxModle);
        }
        #endregion

        #region 彻底删除
        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CompleteDelete(int id)
        {
            AjaxModel ajaxModle = new AjaxModel();
            Permission permission = new Permission();
            try
            {
                IPermissionBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetPermissionBLL;
                bll.DelBy(p => p.ID == id);
                ajaxModle.Statu = "ok";
                ajaxModle.Msg = "删除成功";
            }
            catch
            {
                ajaxModle.Statu = "err";
                ajaxModle.Msg = "删除失败";
            }
            return Json(ajaxModle);
        }
        #endregion
    }
}
