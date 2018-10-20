using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using HKShoppingManage.Common;
using HKShoppingManage.BLL;
using HKShoppingManage.Model;

namespace HKShoppingManage.Web.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        private ICustomerBLL bll;
        public CustomerController(ICustomerBLL _bll)
        {
            this.bll = _bll;
        }
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Customer model)
        {
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.Creator = MvcHelper.User.LoginCode;
            model.Updator = MvcHelper.User.LoginCode;
            var result = await this.bll.Insert(model);
            return RedirectToAction("Index", "Customer");
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int Id)
        {
            var result = await this.bll.Delete(Id);
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public async Task<JsonResult> Update(Customer model)
        {
            model.UpdateTime = DateTime.Now;
            model.Updator = MvcHelper.User.LoginCode;
            var result = await this.bll.Update(model);
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public async Task<JsonResult> IsExist(string cusName, string CusTelephone)
        {
            var result = await this.bll.IsExists(cusName, CusTelephone);
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public async Task<JsonResult> GetModel(int Id)
        {
            var result = await this.bll.GetModel(Id);
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public JsonResult GetList(string HelpCode="")
        {
            //var result = await this.bll.GetList(HelpCode);
            var result = CacheManager.CustomerCache.Values.Where(p => p.HelpCode.Contains(HelpCode));
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public async Task<JsonResult> GetPageList(int pageIndex, int pageSize, string CusName="", string CusTelephone="")
        {
            var result = await this.bll.GetPageList(pageIndex, pageSize, CusName, CusTelephone);
            return Json(new JsonModel(true, "", result));
        }
    }
}