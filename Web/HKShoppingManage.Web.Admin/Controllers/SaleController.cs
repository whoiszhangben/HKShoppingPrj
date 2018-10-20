using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HKShoppingManage.Common;
using HKShoppingManage.Model;
using HKShoppingManage.BLL;

namespace HKShoppingManage.Web.Admin.Controllers
{
    public class SaleController : BaseController
    {
        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }
    }
}