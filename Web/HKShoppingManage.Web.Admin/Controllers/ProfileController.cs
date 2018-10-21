using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using HKShoppingManage.Model;
using HKShoppingManage.BLL;

namespace HKShoppingManage.Web.Admin.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileBLL bll;
        public ProfileController(IProfileBLL _bll)
        {
            this.bll = _bll;
        }
        // GET: Profile
        public ActionResult Index()
        {
            ViewBag.ProfileNo = this.bll.GenerateBillNo();
            ViewBag.CertificationLst = CertificationFactory.model.CertificationLst;
            return View();
        }
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Add(Profile model)
        {
            //todo 验证输入
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.Creator = MvcHelper.User.LoginCode;
            model.Updator = MvcHelper.User.LoginCode;
            var result = await this.bll.Insert(model);
            if (result > 0)
            {
                return Json(new JsonModel(true, "", "新增成功"));
            }
            else
            {
                return Json(new JsonModel(false, "", "新增失败"));
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetList()
        {
            var result = await this.bll.GetList();
            return Json(new JsonModelBootstrapTable(result.Count, result));
        }
    }
}