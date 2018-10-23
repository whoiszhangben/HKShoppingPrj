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
        public async Task<JsonResult> AddOrEdit(Profile model)
        {
            //todo 验证输入
            if (model.Flag == 1)
            {
                model.UpdateTime = DateTime.Now;
                model.Updator = MvcHelper.User.LoginCode;
            }
            else
            {
                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.Creator = MvcHelper.User.LoginCode;
                model.Updator = MvcHelper.User.LoginCode;
            }
            var result = await this.bll.Insert(model);
            if (result > 0)
            {
                return Json(new JsonModel(true, "", (model.Flag == 0) ? "新增成功" : "编辑成功"));
            }
            else
            {
                return Json(new JsonModel(false, "", (model.Flag == 0) ? "新增失败" : "编辑失败"));
            }
        }

        [HttpPost]
        public async Task<List<Profile>> GetList(string name, string idNo, string telNo, int isDimissioned)
        {
            var result = await this.bll.GetList(name, idNo, telNo, isDimissioned);
            return result;
        }

        [HttpPost]
        public async Task<JsonResult> GetListByConditions(int pageIndex, int pageSize, string name, string idNo, string telNo, int isDimissioned)
        {
            PagedList<Profile> list = await this.bll.GetListByConditions(pageIndex, pageSize, name, idNo, telNo, isDimissioned);
            return Json(list);
        }

        public async Task<ActionResult> ExportToExcel(string name, string idNo, string telNo, int isDimissioned)
        {
            var list = await this.bll.GetList(name, idNo, telNo, isDimissioned);
            try
            {
                NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
                NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
                row1.CreateCell(0).SetCellValue("序号");
                row1.CreateCell(1).SetCellValue("档案编号");
                row1.CreateCell(2).SetCellValue("员工名称");
                row1.CreateCell(3).SetCellValue("身份证号码");
                row1.CreateCell(4).SetCellValue("电话号码");
                row1.CreateCell(5).SetCellValue("资料列表");

                for (int i = 0; i < list.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(i + 1);
                    rowtemp.CreateCell(1).SetCellValue(list[i].ProfileNo);
                    rowtemp.CreateCell(2).SetCellValue(list[i].EmpName);
                    rowtemp.CreateCell(3).SetCellValue(list[i].EmpIDNo);
                    rowtemp.CreateCell(4).SetCellValue(list[i].EmpTelNo);
                    rowtemp.CreateCell(5).SetCellValue(list[i].RelationVal);
                }

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                book.Write(ms);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel", string.Format("{0}.xls", "员工资料档案表" + DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string profileNo)
        {
            //更新数据库
            var result = await Task.FromResult(this.bll.Delete(profileNo));
            return Json(new JsonModel(true, ""));
        }
    }
}