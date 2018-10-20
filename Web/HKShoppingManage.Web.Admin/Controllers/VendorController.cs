using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using HKShoppingManage.Common;
using HKShoppingManage.BLL;
using HKShoppingManage.Model;

namespace HKShoppingManage.Web.Admin.Controllers
{
    public class VendorController : BaseController
    {
        private IVendorBLL bll;
        public VendorController(IVendorBLL _bll)
        {
            this.bll = _bll;
        }
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Vendor model)
        {
            var result = await this.bll.Insert(model);
            if (result > 0)
            {
                model.Id = result;
                //更新类别缓存
                CacheManager.VendorCache.SyncCache(result, model, false);
            }
            return RedirectToAction("Index", "Vendor");
        }

        [HttpPost]
        public async Task<JsonResult> GetList()
        {
            var result = await this.bll.GetList();
            return Json(new JsonModelBootstrapTable(result.Count, result));
        }

        public JsonResult GetVendors()
        {
            var result = CacheManager.VendorCache.Values;
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> GetPageList(int pageIndex, int pageSize)
        {
            var result = await this.bll.GetPageList(pageIndex, pageSize);
            return Json(new JsonModel(true, "", result));
        }
    }
}