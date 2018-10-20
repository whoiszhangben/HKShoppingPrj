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
    public class ProductController : BaseController
    {
        private IProductBLL bll;
        public ProductController(IProductBLL _bll)
        {
            this.bll = _bll;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            var defaultCategory = CacheManager.CategoryLstCache.FirstOrDefault().Value.FirstOrDefault();
            List<Category> displayCategory = new List<Category>();
            foreach (var eachItem in CacheManager.CategoryLstCache.Values)
            {
                displayCategory.AddRange(eachItem);
            }
            SelectList selectLst = new SelectList(displayCategory, "Id", "Name", defaultCategory.Name);
            ViewData["CategoryLst"] = selectLst;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Product model)
        {
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.Creator = MvcHelper.User.LoginCode;
            model.Updator = MvcHelper.User.LoginCode;
            model.CategoryName = CacheManager.CategoryCache[model.CategoryId].Name;
            var result = await this.bll.Insert(model);
            if (result > 0)
            {
                model.Id = result;
                //更新类别缓存
                CacheManager.ProductCache.SyncCache(result, model, false);
            }
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public async Task<JsonResult> GetList()
        {

            var result = await this.bll.GetList();
            return Json(new JsonModel(true, "", result));
        }
        public JsonResult GetProducts()
        {
            var result = CacheManager.ProductCache.Values;
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> GetPageList(int pageIndex, int pageSize, string ProductName = "")
        {
            var result = await this.bll.GetPageList(pageIndex, pageSize, ProductName);
            return Json(new JsonModel(true, "", result));
        }
    }
}