using DI;
using FomatModel;
using IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using QX.Entity;

namespace QX.Admin.Logic
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DoLogin(string username, string password, string code)
        {
            User user = new User();
            AjaxModel ajaxModel = new AjaxModel();

            if (code != Session["validateCode"].ToString()) 
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "验证码输入错";
                return Json(ajaxModel);
            }

            IUserBLL bll = SpringHelper.GetObject<IBLLSessionFactory>("BLLSessionFactory").GetBLLSession().GetUserBLL;

            user = bll.GetListBy(u => u.Username == username.Trim()).SingleOrDefault();
            if (user == null) 
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "不存在该用户";
                return Json(ajaxModel);
            }
            password = Common.Encryt.GetMD5(password);
            user = bll.GetListBy(u => u.Username == username && u.Password == password).SingleOrDefault();
            if (user==null)
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "密码错误";
                return Json(ajaxModel);
            }

            ajaxModel.Statu = "ok";
            ajaxModel.Msg = "登陆成功";
            ajaxModel.BackUrl = "/Admin/Home/Index";
            return Json(ajaxModel);
        }


        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }




    }
}
