using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data;
using HKShoppingManage.DBUtility;
using HKShoppingManage.Common;
using HKShoppingManage.Model;
using Dapper;
using System.Web.Http.Cors;

namespace WebAPI
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PurchaseOrderController : ApiController
    {
        [HttpGet]
        public async Task<string> POOrders(bool IsDone=false)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.* from POOrder a left join POOrderDetail b on a.Id=b.Id where a.OrderState!=1 and b.State=" + (IsDone ? 1 : 0));

            List<POOrderDetail> list = new List<POOrderDetail>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<POOrderDetail>(strSql.ToString());
                list = tempList.ToList();
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
        [HttpGet]
        public async Task<string> POOrders(int OrderId,int EntryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.* from POOrder a left join POOrderDetail b on a.Id=b.Id where a.OrderState!=2 and b.Id=" + OrderId + " and b.EntryId=" + EntryId);

            POOrderDetail detail = new POOrderDetail();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<POOrderDetail>(strSql.ToString());
                detail = tempList.ToList().FirstOrDefault();
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(detail);
        }
        [HttpGet]
        public async Task<string> Vendors()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,VendorName from Vendors");

            List<Vendor> list = new List<Vendor>();
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();
                var tempList = await conn.QueryAsync<Vendor>(strSql.ToString());
                list = tempList.ToList();
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
        [HttpPost]
        public async Task<int> Update([FromBody]POOrderDetail model)
        {
            int result = 0;
            using (var conn = DBConnFactory.CreateSqlConnection())
            {
                conn.Open();

                try
                {
                    StringBuilder strSql = new StringBuilder();
                    var param = new DynamicParameters();

                    #region 订单信息
                    strSql.Append("Update POOrderDetail set VendorId=@VendorId,VendorName=@VendorName,Qty=@Qty,Price=@Price,ExRate=@ExRate,Amount=@Qty*@Price,AmountRMB=@Qty*@Price*@ExRate,State=@State,Updator=@Updator,UpdateTime=@UpdateTime ");
                    strSql.Append(" where Id=@Id and EntryId=@EntryId");

                    param.Add("@VendorId", model.VendorId, dbType: DbType.Int32);
                    param.Add("@VendorName", model.VendorName, dbType: DbType.String);
                    param.Add("@Qty", model.Qty, dbType: DbType.Double);
                    param.Add("@Price", model.Price, dbType: DbType.Double);
                    param.Add("@ExRate", model.ExRate, dbType: DbType.Double);
                    param.Add("@State", model.State, dbType: DbType.Int32);
                    param.Add("@UpdateTime", model.UpdateTime, dbType: DbType.DateTime);
                    param.Add("@Updator", model.Updator, dbType: DbType.String);
                    param.Add("@Id", model.Id, dbType: DbType.Int32);
                    param.Add("@EntryId", model.EntryId, dbType: DbType.Int32);

                    await conn.ExecuteAsync(strSql.ToString(), param);

                    #endregion
                    result = model.Id;
                }
                catch (Exception ex)
                {
                    LogHelper.Log.WriteError("[回滚]修改采购订单出错", ex);
                }
                finally
                {
                    if (conn != null)
                        conn.Close();
                }
            }
            return result;
        }
        [HttpOptions]
        public string Options(string value = "")
        {
            return null; // HTTP 200 response with empty body
        }
    }
}
