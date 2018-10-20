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
    public class PurchaseController : BaseController
    {
        private IPOOrderBLL bll;
        public PurchaseController(IPOOrderBLL _bll)
        {
            this.bll = _bll;
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var orderId = Request.QueryString["Id"];
            if (orderId != null)
            {
                var model = this.bll.GetModel(Convert.ToInt32(orderId));
                return View(model);
            }
            return View(new POOrder() { POOrderNo = this.bll.GetBillNo() });
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Create(POOrder model)
        {
            model.Updator = MvcHelper.User.LoginCode;
            model.OrderDetails.ForEach((p) => {
                Product cacheProduct = null;
                CacheManager.ProductCache.TryGetValue(p.ProductId, out cacheProduct);
                p.ProductName = cacheProduct.Name;
                Vendor cacheVendor = null;
                CacheManager.VendorCache.TryGetValue(p.VendorId, out cacheVendor);
                p.VendorName = cacheVendor.VendorName;
            });
            model.UpdateTime = DateTime.Now;
            if (model.Id>0)
            {
                //编辑
                var result = await this.bll.Update(model);
                return Json(new JsonModel(true, "", result));

            }
            else
            {
                //新增
                model.Creator = MvcHelper.User.LoginCode;
                model.CreateTime = DateTime.Now;
                var result = await this.bll.Insert(model);
                return Json(new JsonModel(true, "", result));
            }
        }
        
        [HttpPost]
        public async Task<JsonResult> GetList()
        {
            var result = await this.bll.GetList();
            return Json(new JsonModelBootstrapTable(result.Count,result));
        }

        [HttpPost]
        public async Task<JsonResult> GetPageList(int pageIndex, int pageSize)
        {
            var result = await this.bll.GetPageList(pageIndex, pageSize);
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int Id)
        {
            var result = await this.bll.Delete(Id);
            return Json(new JsonModel(true, "", result));
        }

        public async Task<JsonResult> Confirm(int Id)
        {
            var result = await this.bll.Confirm(Id);
            return Json(new JsonModel(true, "", result));
        }
    }
}