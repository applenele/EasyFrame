using QX.Entity;
using QX.Entity.Models;
using QX.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QX.Admin.Logic
{
    public class BaseController : Controller
    {
        IBLL.IUserBLL bll = OperationContext.GetBLLSessionFactory().GetBLLSession().GetUserBLL;
        public User CurrentUser { set; get; }
        public BaseController()
        {

        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (requestContext.HttpContext.User != null && requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = bll.GetListBy(u => u.Username == requestContext.HttpContext.User.Identity.Name).SingleOrDefault();
                CurrentUser = ViewBag.CurrentUser;
            }
            else
            {
                ViewBag.CurrentUser = null;
                CurrentUser = null;
            }


        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}
