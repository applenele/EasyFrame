﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QX.Admin.Logic
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}