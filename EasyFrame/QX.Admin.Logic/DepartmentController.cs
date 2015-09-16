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
    public class DepartmentController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region 分页的得到部门
        /// <summary>
        /// 分页的得到部门
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public ActionResult GetDepartments(int page, int rows)
        {
            DGModel<vDepartment> dgModel = new DGModel<vDepartment>();
            List<Department> _departments = new List<Department>();
            List<vDepartment> departments = new List<vDepartment>();
            int count = 0;
            try
            {
                IDepartmentBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetDepartmentBLL;

                _departments = bll.LoadPageEntity(page, rows, out count, d => d.IsDelete == false, p => p.AddTime, false).ToList();
                foreach (var department in _departments)
                {
                    departments.Add(new vDepartment(department));
                }
                dgModel.total = count;
                dgModel.rows = departments;
            }
            catch
            {
                dgModel.total = count;
                dgModel.rows = null;
            }
            return Json(dgModel);
        }
        #endregion


        #region 删除部门
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            AjaxModel ajaxModel = new AjaxModel();
            try
            {
                IDepartmentBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetDepartmentBLL;
                bll.DelBy(d => d.ID == id);
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "删除成功";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "删除失败";
            }
            return Json(ajaxModel);
        }
        #endregion


        public ActionResult Add()
        {
            return View();
        }

        #region 执行部门增加
        /// <summary>
        /// 执行部门增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DoAdd(Department model)
        {
            AjaxModel ajaxModel = new AjaxModel();

            try
            {
                IDepartmentBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetDepartmentBLL;
                model.IsDelete = false;
                model.AddTime = DateTime.Now;
                bll.Add(model);
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "增加成功";
                ajaxModel.BackUrl = "/Admin/Department/Index";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "增加失败";
            }
            return Json(ajaxModel);
        }
        
        #endregion


        public ActionResult Update(int id)
        {
            Department department = new Department();
            IDepartmentBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetDepartmentBLL;
            department = bll.GetListBy(d => d.ID == id).SingleOrDefault();
            ViewBag.Department = department;
            return View();
        }

        #region MyRegio执行修改n
        /// <summary>
        /// 执行修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DoUpdate(Department model)
        {
            Department department = new Department();
            AjaxModel ajaxModel = new AjaxModel();
            try
            {
                IDepartmentBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetDepartmentBLL;
                department = bll.GetListBy(d => d.ID == model.ID).SingleOrDefault();
                department.DepartmentName = model.DepartmentName;
                department.Remark = model.Remark;
                bll.Modify(department, "DepartmentName", "Remark");
                ajaxModel.Statu = "ok";
                ajaxModel.Msg = "修改成功!";
                ajaxModel.BackUrl = "/Admin/Department/Index";
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "修改失败!";
            }

            return Json(ajaxModel);
        } 
        #endregion
    }
}
