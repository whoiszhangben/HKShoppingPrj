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
    public class PhotoController : Controller
    {
        private IAlbumBLL bll;
        public PhotoController(IAlbumBLL _bll)
        {
            this.bll = _bll;
        }
        // GET: Photo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Add(Album model)
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
            var list = await this.bll.GetList();
            return Json(new JsonModel(true, "", list));
        }
    }
}