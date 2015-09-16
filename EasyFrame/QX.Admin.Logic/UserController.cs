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
    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region 显示用户
        /// <summary>
        /// 显示用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Show(int id)
        {
            User user = new User();
            try
            {
                IUserBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserBLL;
                user = bll.GetListBy(u => u.ID == id).SingleOrDefault();
                ViewBag.User = user;
            }
            catch { }

            return View();
        }

        #endregion

        #region 展示增加页面
        /// <summary>
        ///  展示增加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            List<SelectListItem> genderLst = new List<SelectListItem>();
            List<SelectListItem> departmentLst = new List<SelectListItem>();
            List<Department> departments = new List<Department>();
            genderLst.Add(new SelectListItem { Text = "男", Value = "0", Selected = true });
            genderLst.Add(new SelectListItem { Text = "女", Value = "1", Selected = true });

            try
            {
                IDepartmentBLL departmentBLL = OperationContext.GetBLLSessionFactory().GetBLLSession().GetDepartmentBLL;
                departments = departmentBLL.GetListBy(d => d.IsDelete == false);
                foreach (var department in departments)
                {
                    departmentLst.Add(new SelectListItem { Text = department.DepartmentName, Value = department.ID.ToString(), Selected = false });
                }
            }
            catch
            {

            }
            ViewBag.Departments = departmentLst;
            ViewBag.Gender = genderLst;
            return View();
        }
        #endregion

        #region 执行增加
        /// <summary>
        /// 执行增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DoAdd(User model)
        {
            AjaxModel ajaxModel = new AjaxModel();
            try
            {
                IUserBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserBLL;
                model.AddTime = DateTime.Now;
                model.IsDelete = false;
                model.Password = Common.Encryt.GetMD5(model.Password);
                bll.Add(model);
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "增加成功！";
                ajaxModel.BackUrl = "/Admin/User/Index";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "增加失败！";
            }
            return Json(ajaxModel);
        }

        #endregion


        #region 展示更新页面
        /// <summary>
        /// 展示更新页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            User user = new User();
            List<SelectListItem> genderLst = new List<SelectListItem>();
            List<SelectListItem> departmentLst = new List<SelectListItem>();
            List<Department> departments = new List<Department>();

            try
            {
                IUserBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserBLL;
                user = bll.GetListBy(u => u.ID == id).SingleOrDefault();
                genderLst.Add(new SelectListItem { Text = "男", Value = "0", Selected = user.Gender == 0 ? true : false });
                genderLst.Add(new SelectListItem { Text = "女", Value = "1", Selected = user.Gender == 1 ? true : false });
                IDepartmentBLL departmentBLL = OperationContext.GetBLLSessionFactory().GetBLLSession().GetDepartmentBLL;
                departments = departmentBLL.GetListBy(d => d.IsDelete == false);
                foreach (var department in departments)
                {
                    departmentLst.Add(new SelectListItem { Text = department.DepartmentName, Value = department.ID.ToString(), Selected = user.DepartmentID == department.ID ? true : false });
                }

            }
            catch
            { }
            ViewBag.Departments = departmentLst;
            ViewBag.Gender = genderLst;
            ViewBag.User = user;
            return View();
        }
        #endregion

        #region 执行修改
        /// <summary>
        /// 执行修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DoUpdate(User model)
        {
            AjaxModel ajaxModel = new AjaxModel();
            User user = new User();
            try
            {
                IUserBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserBLL;
                user = bll.GetListBy(u => u.ID == model.ID).SingleOrDefault();
                user.Username = model.Username;
                user.DepartmentID = model.DepartmentID;
                user.Gender = model.Gender;
                user.Remark = model.Remark;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    user.Password = Common.Encryt.GetMD5(model.Password);
                }
                bll.Modify(user, "Username", "DepartmentID", "Gender", "Remark", "Password");
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "修改成功！";
                ajaxModel.BackUrl = "/Admin/User/Index";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "修改失败！";
            }
            return Json(ajaxModel);
        }
        #endregion


        #region 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            AjaxModel ajaxModel = new AjaxModel();
            try
            {
                IUserBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserBLL;
                int result = bll.DelBy(u => u.ID == id);
                if (result > 0)
                {
                    ajaxModel.Statu = "ok";
                    ajaxModel.Msg = "删除成功！";
                }
                else
                {
                    ajaxModel.Statu = "err";
                    ajaxModel.Msg = "删除失败！";
                }
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "删除失败！";
            }
            return Json(ajaxModel);
        }
        #endregion

        #region 按页获取用户
        /// <summary>
        /// 按页获取用户
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetUsers(int page, int rows)
        {
            DGModel<User> dgModel = new DGModel<User>();
            List<User> users = new List<User>();
            int count = 0;
            try
            {
                IUserBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserBLL;
                users = bll.LoadPageEntity(page, rows, out count, u => u.IsDelete == false, u => u.AddTime, false).ToList();
                dgModel.rows = users;
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

        #region 得到用户角色 + GetUserRoles
        /// <summary>
        /// 得到用户角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserRoles(int uid)
        {
            DGModel<vRole> dgModel = new DGModel<vRole>();
            List<UserRole> userRoles = new List<UserRole>();
            List<vRole> vroles = new List<vRole>();
            Role role = new Role();
            try
            {
                IUserRoleBLL urbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserRoleBLL;
               
                IRoleBLL rbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetRoleBLL;
              
                userRoles = urbll.GetListBy(ur => ur.UserID == uid && ur.IsDelete == false, ur => ur.AddTime);

                foreach (var ur in userRoles)
                {
                    role = rbll.GetListBy(r => r.ID == ur.RoleID).SingleOrDefault();
                    vroles.Add(new vRole(role));
                }
                dgModel.rows = vroles;
                dgModel.total = vroles.Count();
            }
            catch
            {
                dgModel.rows = null;
                dgModel.total = 0;
            }
            return Json(dgModel);
        }
        #endregion


        #region 给用户授权角色
        /// <summary>
        /// 给用户授权角色
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult InvokeRole(int rid, int uid)
        {
            AjaxModel ajaxModel = new AjaxModel();
            UserRole ur = new UserRole();
            try
            {
                IUserRoleBLL urbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserRoleBLL;
                UserRole temp = new UserRole();
                temp = urbll.GetListBy(userrole => userrole.RoleID == rid && userrole.UserID == uid).SingleOrDefault();
                if (temp == null)
                {
                    ur.UserID = uid;
                    ur.RoleID = rid;
                    ur.AddTime = DateTime.Now;
                    ur.IsDelete = false;
                    urbll.Add(ur);
                    ajaxModel.Statu = "ok";
                    ajaxModel.Msg = "增加成功！";
                }
                else
                {
                    ajaxModel.Statu = "err";
                    ajaxModel.Msg = "该用户已经存在改角色！请选择别的角色！";
                }
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "增加失败！";
            }
            return Json(ajaxModel);
        }
        #endregion


        #region 删除角色 +RevokeRole(int rid, int uid)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult RevokeRole(int rid, int uid)
        {
            AjaxModel ajaxModle = new AjaxModel();
            try
            {
                IUserRoleBLL urbll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserRoleBLL;
                int result = urbll.DelBy(ur => ur.UserID == uid && ur.RoleID == rid);
                if (result > 0)
                {
                    ajaxModle.Statu = "ok";
                    ajaxModle.Msg = "删除成功！";
                }
                else
                {
                    ajaxModle.Statu = "err";
                    ajaxModle.Msg = "删除失败！";
                }
            }
            catch
            {
                ajaxModle.Statu = "err";
                ajaxModle.Msg = "删除失败！";
            }
            return Json(ajaxModle);
        }
        #endregion


    }
}
