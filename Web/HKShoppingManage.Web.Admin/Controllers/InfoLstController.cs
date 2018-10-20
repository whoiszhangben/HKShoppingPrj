using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HKShoppingManage.Web.Admin.Controllers
{
    public class InfoLstController : Controller
    {
        // GET: InfoLst
        public ActionResult Index()
        {
            ViewBag.CertificationLst = CertificationFactory.model.CertificationLst;
            return View();
        }
    }
}