using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HKShoppingManage.Common;
using HKShoppingManage.BLL;
using HKShoppingManage.Model;

namespace HKShoppingManage.Web.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}