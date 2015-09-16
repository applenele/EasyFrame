using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QX.Web.Filters
{
    public class AdminLoginValidateAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!QX.Helper.OperationContext.IsLogin())
            {
                filterContext.Result = new RedirectResult("/Admin/Login/Login");
            }
        }
    }
}