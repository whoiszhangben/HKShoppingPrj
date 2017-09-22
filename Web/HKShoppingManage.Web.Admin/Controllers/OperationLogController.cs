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
    public class OperationLogController : BaseController
    {
        private IOperationLogBLL bll;
        public OperationLogController(IOperationLogBLL bll)
        {
            this.bll = bll;
        }
        // GET: OperationLog
        public ActionResult Index()
        {
            return View();
        }
        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="logType">日志类型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetList(int pageIndex, int pageSize, DateTime? startDate, DateTime? endDate, int? logType, string AssetCode)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageSize < 1)
                pageSize = 10;

            //获取
            PagedList<OperationLog> list = await this.bll.GetPageList(pageIndex, pageSize, startDate, endDate, logType, AssetCode);

            return Json(new JsonModel(true, "", list));
        }
        #endregion
    }
}